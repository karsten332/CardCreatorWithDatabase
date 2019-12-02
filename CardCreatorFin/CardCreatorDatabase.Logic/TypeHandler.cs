using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardCreatorDatabase.Domain;
using CardCreatorDatabase.Data;


namespace CardCreatorDatabase.Logic
{
  public static class TypeHandler
    {
        static public void CreateType( string typeName,int minStat = 0,int maxStat = 99)
        {
            var newType = new Type1()
            {
                Name = typeName,
                MinStat = minStat,
                MaxStat = maxStat,
            };

            using (DatabaseContext context = new DatabaseContext())
            {
                context.Types.Add(newType);
                context.SaveChanges();
            }
        }

        static public List<Type1> GetTypeList()
        {
            
            using (DatabaseContext context = new DatabaseContext())
            {
                return context.Types.ToList();
                
            }
            
        }
    }
   
     
        
        
}
