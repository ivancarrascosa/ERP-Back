using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Proveedor
    {
        public int IdProveedor { get; }
        public string NombreEmpresa { get; }
        public string Telefono { get; }
        public string Email { get; }
        public string Direccion { get; }

        public Proveedor(int idProveedor, string nombreEmpresa, string telefono,
            string email, string direccion)
        {
            IdProveedor = idProveedor;
            NombreEmpresa = nombreEmpresa;
            Telefono = telefono;
            Email = email;
            Direccion = direccion;
        }
    }
}
