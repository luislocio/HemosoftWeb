﻿using HemoSoft.Models;
using HemosoftWeb.Models;

namespace HemosoftWeb.DAL
{
    class SingletonUsuario
    {
        private SingletonUsuario() { }

        private static Usuario usuario;

        public static Usuario GetInstance()
        {
            if (usuario == null)
            {
                usuario = new Usuario();
            }
            return usuario;
        }
    }
}