namespace DojoDachi.Models
{
    public class Pet{
        
    
        public int Fullness {get; set;}
        public int Happiness{get; set;}
        public int Energy{get; set;} 
        public string Name{get; set;}
        public int Meals{get; set;}

        public Pet(string name){
            Fullness = 20;
            Happiness = 20;
            Energy = 50;
            Name = name;
            Meals = 3;
        } 


        
    }
    
}