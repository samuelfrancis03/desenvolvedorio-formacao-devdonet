using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CursoEFCore.Entities
{
    [Table("Clientes")] //Informar o nome na tabela
    internal class Cliente
    {
        //DataAnnotations
        [Key] //Chave Primaria
        public int Id { get; set; }
        [Required] //Campo Obrigatorio
        public string Nome { get; set; }
        [Column("Phone")] //Mapear a propiedade com nome na tabela do BD.
        public string Telefone { get; set; }
        public string CEP { get; set; }
        public string Estado { get; set; }
        public string Cidade { get; set; }


    }
}
