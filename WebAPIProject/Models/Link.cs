namespace WebAPIProject.Models
{
    public class Link
    {
        public string Href { get; set; }
        public string Method { get; set; }
        public string Rel { get; set; }

        public Link(string href, string method, string rel)
        {
            Href = href;
            Method = method;
            Rel = rel;
        }
    }

}
