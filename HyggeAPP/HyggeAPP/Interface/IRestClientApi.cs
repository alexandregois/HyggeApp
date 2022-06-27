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

        [Post("/api/EnderecoCliente/Alterar?token={token}")]
        Task<UsuarioEnderecoModel> AtualizaCadEndereco([Body] UsuarioEnderecoModel clienteModel, [AliasAs("token")] string token);

        [Post("/api/EnderecoCliente/InativarEnderecoCliente?token={token}")]
        Task<UsuarioEnderecoModel> InativarCadEndereco([Body] UsuarioEnderecoModel clienteModel, [AliasAs("token")] string token);

        //[Get("/posts/{id}")]
        //Task<UsuarioModel> GetAll(int id);

        [Post("/api/EnderecoCliente/Listar?token={token}&usuario_rec_id={usuario_rec_id}")]
        Task<List<UsuarioEnderecoModel>> PostListaEndereco([AliasAs("token")] string token, [AliasAs("usuario_rec_id")] int usuario_rec_id);

        [Post("/api/VeiculoCliente/Listar?token={token}&usuario_rec_id={usuario_rec_id}")]
        Task<List<VeiculoClienteModel>> PostListaVeiculo([AliasAs("token")] string token, [AliasAs("usuario_rec_id")] int usuario_rec_id);

        [Post("/api/VeiculoCliente/Cadastrar?token={token}")]
        Task<VeiculoClienteModel> PostVeiculo([Body] VeiculoClienteModel veiculoCliente, [AliasAs("token")] string token);

        [Post("/api/VeiculoCliente/Alterar?token={token}")]
        Task<VeiculoClienteModel> AlterarVeiculo([Body] VeiculoClienteModel veiculoCliente, [AliasAs("token")] string token);

        [Post("/api/VeiculoCliente/Inativar?token={token}")]
        Task<VeiculoClienteModel> InativarVeiculo([Body] VeiculoClienteModel veiculoCliente, [AliasAs("token")] string token);

        [Post("/api/MarcaVeiculos/Listar?token={token}")]
        Task<List<MarcaVeiculoModel>> PostVeiculoListaMarcas([AliasAs("token")] string token);

        [Post("/api/MarcaVeiculos/ListarModelosBy_marca_rec_id?token={token}&marca_rec_id={marca_rec_id}")]
        Task<List<MarcaVeiculoModel>> PostVeiculoListaModelos([AliasAs("token")] string token, [AliasAs("marca_rec_id")] int marca_rec_id);

        [Post("/api/MarcaVeiculos/Listar?token={token}")]
        Task<List<MarcaVeiculoModel>> ListarMarcasVeiculos([AliasAs("token")] string token);

        [Post("/api/MarcaVeiculos/ListarModelosBy_marca_rec_id?token={token}&marca_rec_id={marca_rec_id}")]
        Task<List<ModeloVeiculoModel>> ListarModelosVeiculosByMarca([AliasAs("token")] string token, [AliasAs("marca_rec_id")] int marca_rec_id);




    }
}
