// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

ReadFile();
//Console.ReadKey(); 

static void ReadFile()
{
    string file = @"C:\Users\digis\Documents\Jacqueline Juarez Ramirez\Block de notas\LayoutUsuario.txt";

    if (File.Exists(file))
    {

        StreamReader Textfile = new StreamReader(file);

        string line;
        line = Textfile.ReadLine();

        ML.Result resultErrores = new ML.Result();
        resultErrores.Objects = new List<object>();

        while ((line = Textfile.ReadLine()) != null)
        {
            string[] lines = line.Split('|');

            ML.Usuario usuario = new ML.Usuario();

            usuario.Nombre = lines[0];
            usuario.ApellidoPaterno = lines[1];
            usuario.ApellidoMaterno = lines[2];
            usuario.FechaNacimiento = lines[3];
            usuario.UserName = lines[4];
            usuario.Password = lines[5];
            usuario.Sexo = lines[6];
            usuario.Telefono = lines[7];
            usuario.Celular = lines[8];
            usuario.Curp = lines[9];
            usuario.Email = lines[10];

            usuario.Rol = new ML.Rol();
            usuario.Rol.IdRol = byte.Parse(lines[11]);

            usuario.Imagen = null;

            usuario.Direccion = new ML.Direccion();
            usuario.Direccion.Calle = lines[12];

            usuario.Direccion.NumeroInterior = lines[13];

            usuario.Direccion.NumeroExterior = lines[14];

            usuario.Direccion.Colonia = new ML.Colonia();
            usuario.Direccion.Colonia.IdColonia = int.Parse(lines[15]);

            ML.Result result = BL.Usuario.Add(usuario);

            Console.WriteLine("Correcto");
            Console.ReadKey();

            if (!result.Correct)
            {

                resultErrores.Objects.Add(
                    "Mensaje :" +result.ErrorMessage);
                    //"Error no se inserto el nombre"+usuario.Nombre +
                    //"Error no se inserto el ApellidoPaterno" +usuario.ApellidoPaterno +
                    //"Error no se inserto el ApellidoMaterno" +usuario.ApellidoMaterno +
                    //"Error no se inserto la fecha de nacimiento" + usuario.FechaNacimiento +
                    //"Error no se inserto el UserName" + usuario.UserName +
                    //"Error no se inserto el Password" + usuario.Password +
                    //"Error no se inserto el Sexo" + usuario.Sexo +
                    //"Error no se inserto el Telefono" + usuario.Telefono +
                    //"Error no se inserto el Celular" + usuario.Celular +
                    //"Error no se inserto el Email" + usuario.Email +
                    //"Error no se inserto el IdRol" + usuario.Rol.IdRol +
                    //"Error no se inserto la Calle" + usuario.Direccion.Calle +
                    //"Error no se inserto el NumeroInterior" + usuario.Direccion.NumeroInterior +
                    //"Error no se inserto el NumeroExterior" + usuario.Direccion.NumeroExterior +
                    //"Error no se inserto el IdColonia" + usuario.Direccion.Colonia.IdColonia);
            }
        }

        if (resultErrores.Objects != null)
        {

        }
        TextWriter tx = new StreamWriter(@"C:\Users\digis\Documents\Jacqueline Juarez Ramirez\Block de notas\ErroresLayoutUsuario.txt");
        foreach (string error in resultErrores.Objects)
        {
            tx.WriteLine(error);
        }
        tx.Close();
    }
}