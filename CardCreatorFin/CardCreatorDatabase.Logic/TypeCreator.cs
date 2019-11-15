using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardCreatorDatabase.Domain;
using CardCreatorDatabase.Data;


namespace CardCreatorDatabase.Logic
{
  public static class TypeCreator
    {
        static public void CreateType( string typeName)
        {
            var newType = new Type1()
            {
                Name = typeName
            };

            using (DatabaseContext context = new DatabaseContext())
            {
                context.Types.Add(newType);
                context.SaveChanges();

                foreach (var item in context.Types)
                {
                    //Console.WriteLine("{0} {1}", item.Id, item.Name);
                }
            }
        }
    }
   
     
        
        
}
