﻿using System;
using System.Collections.Generic;
using System.Text;
using MTParcial2.Model;
using SQLite;

namespace MTParcial2.ViewModel
{
    public class NotesViewModel
    {
        private SQLiteConnection conec;
        private static NotesViewModel instance;
        public static NotesViewModel Instance
        {
            get
            {
                if (instance == null) { throw new Exception("Falta inicializar"); }
                return instance;
            }
        }

        public static void Inicializador(String filename)
        {
            if (filename == null) { throw new ArgumentException(); }
            if (instance != null) { instance.conec.Close(); }
            instance = new NotesViewModel(filename);
        }

        private NotesViewModel(String DbPath)
        {
            conec = new SQLiteConnection(DbPath);
            conec.CreateTable<Notes>();
        }
        public String EstadoMensaje;

        public int AddNewVenta(string contents)
        {
            int result = 0;

            try
            {
                result = conec.Insert(new Notes()
                {
                    Contents = contents
                });
                EstadoMensaje = string.Format("Nota Ingresada");
            }
            catch (Exception e)
            {
                EstadoMensaje = e.Message;
            }
   
            return result;
        }

    }
}
