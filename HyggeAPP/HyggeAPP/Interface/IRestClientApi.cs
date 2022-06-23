using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SharedTools.Models.WebPortal_API;

namespace HyggeAPP.Interface
{
    public interface IRestClientApi
    {
        [Post("/api/Account/Login")]
        Task<UsuarioModel> PostLogin([Body] UsuarioModel login);

        [Post("/api/Cliente/Cadastrar?token={token}")]
        Task <UsuarioModel> PostCadCliente([Body] ClienteModel clienteModel, [AliasAs("token")] string token);

        [Post("/api/EnderecoCliente/ConsultaCEP?token={token}&cep={cep}")]
        Task<ConsultaCepModel> PostConsultaCEP([AliasAs("token")] string token, [AliasAs("cep")] string cep);

        [Post("/api/EnderecoCliente/Cadastrar?token={token}")]
        Task<UsuarioEnderecoModel> PostCadEndereco([Body] UsuarioEnderecoModel clienteModel, [AliasAs("token")] string token);

        //[Get("/posts/{id}")]
        //Task<UsuarioModel> GetAll(int id);

        [Post("/api/EnderecoCliente/Listar?token={token}&usuario_rec_id={usuario_rec_id}")]
        Task<UsuarioEnderecoModel> PostListaEndereco([AliasAs("token")] string token, [AliasAs("usuario_rec_id")] int usuario_rec_id);

    }
}
