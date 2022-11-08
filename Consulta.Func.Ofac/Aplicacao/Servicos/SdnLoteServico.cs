using System.Net.Http.Headers;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;
using Consulta.Func.Ofac.Aplicacao.Config;
using Consulta.Func.Ofac.Aplicacao.DTO;
using Consulta.Func.Ofac.Aplicacao.Mapper;
using Consulta.Func.Ofac.Aplicacao.Servicos.Interfaces;
using Consulta.Func.Ofac.Dominio.Entidades;
using Consulta.Func.Ofac.Infra.BancoDados.Repositorios.Interfaces;
using Microsoft.Extensions.Options;

namespace Consulta.Func.Ofac.Aplicacao.Servicos
{
    public class SdnLoteServico : ServicoBase, ISdnLoteServico
    {
        private readonly ISdnLoteRepositorio _dsnLoteRepositorio;
        private readonly ISdnServico _sdnServico;
       
        private readonly AppConfig _config;

        public SdnLoteServico(ISdnLoteRepositorio sdnLoteRepositorio, ISdnServico sdnServico, IOptions<AppConfig> options)
        {
            _dsnLoteRepositorio = sdnLoteRepositorio;
            _sdnServico = sdnServico;
            _config = options.Value;
        }

        public int Adicionar(SdnLote obj)
        {
            obj.Validar();

            int idLista = _dsnLoteRepositorio.Adicionar(obj);

            return idLista;
        }

        public int BuscarPorLote(SdnLote obj)
        {
            throw new NotImplementedException();
        }

        
        public async Task <bool> AtualizarRegistrosDaBase(bool EhConsolidado)
        {
            var ret = false;
        
            //Requisição donwload lista site OPAC
            var listaAtualizadaSdnLote = BuscarListaSdnNaOfac(EhConsolidado);

            var sdnLote = SdnLoteMapper.ConverterListaOfacSdnParaListaOfacSdnDto(listaAtualizadaSdnLote.publshInformation,
                    "teste", EhConsolidado);
            var idSdnLote = Adicionar(sdnLote);
            //Mapeia o retorno da lista p/ DTO  p/ fazer a inclusão na base
            var listaMapeadaParaAtualizar = SdnMapper.ConverterListaOfacSdnDtoParaListaOfacSdn
                (listaAtualizadaSdnLote.sdnEntry, idSdnLote);
           

            return await _sdnServico.AdicionarLote(listaMapeadaParaAtualizar);
        
     
        }

        private SdnListDto BuscarListaSdnNaOfac(bool EhConsolidado)
        {
            SdnListDto Listas = null;

            var url = EhConsolidado ? _config.EndPointListaSdn : _config.EndPointListaNaoSdn;
            using HttpClient client = new();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync(url).Result;

            string result = response.Content.ReadAsStringAsync().Result;


            result = result.Replace(
                "\r\n<sdnList xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns=\"http://tempuri.org/sdnList.xsd\">",
                "<sdnList>");
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