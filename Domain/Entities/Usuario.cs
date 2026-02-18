using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Usuario
    {
        public string Email { get; }
        public string idFirebaseUid { get; }
        public string Nombre { get; }

        public Usuario(string email, string firebaseUID, string nombre)
        {
            Email = email;
            idFirebaseUid = firebaseUID;
            Nombre = nombre;
        }
    }
}
