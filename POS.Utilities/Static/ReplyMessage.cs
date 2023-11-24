﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Utilities.Static
{
    public class ReplyMessage
    {
        public const string MESSAGE_QUERY = "Consulta exitosa.";
        public const string MESSAGE_QUERY_EMPTY = "No se encontraron registros.";
        public const string MESSAGE_SAVE = "Se registró correctamente.";
        public const string MESSAGE_UPDATE = "Se actualizó correctamente.";
        public const string MESSAGE_DELTE = "Se eliminó correctamente.";
        public const string MESSAGE_EXISTS = "El registro ya existe.";
        public const string MESSAGE_ACTIVATE = "Token generado correctamente.";
        public const string MESSAGE_VALIDATE = "Errores de validación.";
        public const string MESSAGE_FAILED = "Operación fallida.";
        public const string MESSAGE_TOKEN = "Token generado.";
        public const string MESSAGE_TOKEN_ERROR = "El usuario y/o contraseña es incorrecta.";
        public const string MESSAGE_EXCEPTION = "Hubo un error inesperado, comunicarse con el administrador (admin@gmail.com)";
    }
}
