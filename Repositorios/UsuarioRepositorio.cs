using Microsoft.EntityFrameworkCore;
using SistemasDeTarefas.Data;
using SistemasDeTarefas.Models;
using SistemasDeTarefas.Repositorios.Interfaces;

namespace SistemasDeTarefas.Repositorios
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly SistemaTarefasDBContex _dBContex;

        public UsuarioRepositorio(SistemaTarefasDBContex sistemaTarefasDBContex)
        {
            _dBContex = sistemaTarefasDBContex;
        }

        public async Task<UsuarioModel> BuscarId(int id)
        {
            return await _dBContex.Usuarios.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<UsuarioModel>> BuscarTodosUsuarios()
        {
            return await _dBContex.Usuarios.ToListAsync();
        }

        public async Task<UsuarioModel> Adicionar(UsuarioModel usuario)
        {
            _dBContex.Usuarios.Add(usuario);
            _dBContex.SaveChanges();

            return usuario;
        }

        public async Task<UsuarioModel> Atualizar(UsuarioModel usuario, int id)
        {
            UsuarioModel usuarioPorId = await BuscarId(id);

            if(usuarioPorId == null)
            {
                throw new Exception($"Usuario para o ID:{id} não foi encontrado no banco de dados.");
            }

            usuarioPorId.Nome = usuario.Nome;
            usuarioPorId.Email = usuario.Email;

            _dBContex.Usuarios.Update(usuarioPorId);
            await _dBContex.SaveChangesAsync();

            return usuarioPorId;
        }

        public async Task<bool> Apagar(int id)
        {
            UsuarioModel usuarioPorId = await BuscarId(id);

            if (usuarioPorId == null)
            {
                throw new Exception($"Usuario para o ID:{id} não foi encontrado no banco de dados.");
            }

            _dBContex.Usuarios.Remove(usuarioPorId);
            await _dBContex.SaveChangesAsync();

            return true;
        }
    }
}
