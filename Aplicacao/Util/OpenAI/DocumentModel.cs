using Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacao.Util.OpenAI
{
    public class DocumentModel
    {
        public string id { get; set; }
        public DateTime upload_at { get; set; }
        public string hash { get; set; }
        public string sourcefile { get; set; }
        public string content { get; set; }
        public string metadata_storage_content_type { get; set; }
        public int metadata_storage_size { get; set; }
        public DateTime metadata_storage_last_modified { get; set; }
        public string metadata_storage_content_md5 { get; set; }
        public string metadata_storage_name { get; set; }
        public string metadata_storage_path { get; set; }
        public string metadata_storage_file_extension { get; set; }
        public string metadata_content_type { get; set; }
        public string metadata_language { get; set; }
        public string metadata_author { get; set; }
        public string metadata_title { get; set; }
        public string metadata_creation_date { get; set; }
        public string[] people { get; set; }
        public string[] organizations { get; set; }
        public string[] locations { get; set; }
        public string[] keyphrases { get; set; }
        public string language { get; set; }
        public string translated_text { get; set; }
        public Entidades[] pii_entities { get; set; }
        public string masked_text { get; set; }
        public string merged_content { get; set; }
        public string[] text { get; set; }
        public string[] layoutText { get; set; }
        public string[] imageTags { get; set; }
        public string[] imageCaption { get; set; }
        public string category { get; set; }
        public string sourcepage { get; set; }
    }
    public partial class Entidades
    {
        public string text { get; set; }
        public string type { get; set; }
        public string subtype { get; set; }
        public int offset { get; set; }
        public int length { get; set; }
        public double score { get; set; }
        public string city { get; set; }

    }
}
