namespace Dominio.Entidades
{
    public class TemaConfiguracao
    {

        public TemaConfiguracao()
        {
            Id = Guid.NewGuid().ToString();
            DarkMode = false;
            Layout = "vertical";
            LogoBg = "skin6";
            NavbarBg = "skin6";
            SidebarType = "mini-sidebar";
            SidebarColor = "skin6";
            Components = "skin6";
            SidebarPosition = true;
            HeaderPosition = true;
            BoxedLayout = false;
        }
        public string Id { get; set; }
        //isso pode ser verdadeiro ou falso (verdadeiro significa escuro e falso significa claro),
        public bool DarkMode { get; set; }
        public string Layout { get; set; }

        // Você pode alterar o valor para skin5/skin2/skin3/skin4/skin5/skin6
        public string LogoBg { get; set; }

        // Você pode alterar o valor para skin5/skin2/skin3/skin4/skin5/skin6
        public string NavbarBg { get; set; }

        // Você pode alterá-lo / mini-sidebar / iconbar / overlay
        public string SidebarType { get; set; }

        // Você pode alterar o valor para skin5/skin2/skin3/skin4/skin5/skin6
        public string SidebarColor { get; set; }

        // Você pode alterar o valor para skin5/skin2/skin3/skin4/skin5/skin6
        public string Components { get; set; }

        // Você pode alterá-lo  true / false ( true significa Fixo e falso significa absoluto )
        public bool SidebarPosition { get; set; }

        // Você pode alterá-lo  true / false ( true significa Fixo e falso significa absoluto )
        public bool HeaderPosition { get; set; }

        // pode ser true / false ( true significa Boxed e false significa Fluid )
        public bool BoxedLayout { get; set; }
        public virtual Usuario Usuario { get; set; }
        public string UsuarioId { get; set; }

        public object SerializarParaAtualizar()
        {
            return new
            {
                DarkMode,
                LogoBg,
                NavbarBg,
                SidebarColor,
                SidebarType,
                SidebarPosition,
                Components
            };
        }
    }
}
