using System.Net.Http.Headers;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;
using Consulta.Func.Ofac.Aplicacao.Config;
using Consulta.Func.Ofac.Aplicacao.DTO;
using Consulta.Func.Ofac.Aplicacao.Servicos.Interfaces;
using Consulta.Func.Ofac.Dominio.Entidades;
using Consulta.Func.Ofac.Infra.BancoDados.Repositorios.Interfaces;
using Microsoft.Extensions.Options;
//using Consulta.Func.Ofac.Aplicacao.Mapper;

namespace Consulta.Func.Ofac.Aplicacao.Servicos
{
    public class ListaOfacSdnServico : ServicoBase, IListaOfacSdnServico
    {
        private readonly IListaOfacSdnRepositorio _dsnRepositorio;
        private readonly AppConfig _config;

        public ListaOfacSdnServico(IListaOfacSdnRepositorio listaOfacSdnRepositorio, IOptions<AppConfig> options)
        {
            _dsnRepositorio = listaOfacSdnRepositorio;
            _config = options.Value;
        }

        public int Adicionar(ListaOfacSdn obj)
        {
            obj.Validar();

            int idLista = _dsnRepositorio.Adicionar(obj);

            return idLista;
        }

        public bool AtualizarRegistrosDaBase()
        {
            var ret = false;

            //Requisição donwload lista site OPAC
            var listaAtualizadaOpacSdn = AlualizarLista();

            //Mapeia o retorno da lista p/ DTO  p/ fazer a inclusão na base
            var listaMapeadaParaAtualizar = new List<ListaOfacSdn>();
            //ListaSdnMapper.ConverterListaOfacSdnDtoParaListaOfacSdn(listaAtualizadaOpacSdn);
            AdicionarLista(listaMapeadaParaAtualizar);

            ret = true;

            return ret;
        }

        private SdnListDto AlualizarLista()
        {
            SdnListDto Listas = null;

            using HttpClient client = new();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync(_config.EndPointListaSdn).Result;

            string result = response.Content.ReadAsStringAsync().Result;

               
            result = result.Replace("\r\n<sdnList xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns=\"http://tempuri.org/sdnList.xsd\">", "<sdnList>");
            result = result.Replace("<?xml version=\"1.0\" standalone=\"yes\"?>", "");
            result = result.Replace("</xml>", "");

            XDocument doc = XDocument.Parse(result);

            var stream = new MemoryStream(Encoding.UTF8.GetBytes(result ?? ""));


            using (StreamReader reader = new StreamReader(stream))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(SdnListDto));


                Listas = (SdnListDto)serializer.Deserialize(reader);
            }

            return Listas;
        }

        // public ListaOfacSdn BuscarPorIdSdn(int idSdn)
        // {
        //     if (idSdn == 0)
        //         Notificar("O Id deve ser informado.");
        //
        //     return _dsnRepositorio.BuscarPorIdSdn(idSdn);
        // }
        //
        // public List<ListaOfacSdn> Listar()
        // {
        //     return _dsnRepositorio.Listar();
        // }

        private void AdicionarLista(List<ListaOfacSdn> lista)
        {
            lista.ForEach(listaOfac =>
            {
                try
                {
                    Adicionar(listaOfac);
                }
                catch (Exception)
                {
                }
            });
        }
        protected T FromXml<T>(String xml)
        {
            T returnedXmlClass = default(T);

            try
            {
                using (TextReader reader = new StringReader(xml))
                {
                    try
                    {
                        returnedXmlClass =
                            (T)new XmlSerializer(typeof(T)).Deserialize(reader);
                    }
                    catch (InvalidOperationException)
                    {
                        // String passed is not XML, simply return defaultXmlClass
                    }
                }
            }
            catch (Exception ex)
            {
            }

            return returnedXmlClass;
        }
    }
}