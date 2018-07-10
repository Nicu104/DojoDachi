using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DojoDachi.Models;
using Microsoft.AspNetCore.Http;

namespace DojoDachi.Controllers
{
    public class HomeController : Controller{


        public static Pet pet = new Pet("Bobic");
        public static Random rand = new Random();
        
        public IActionResult Index(){
            string message = "";
            message = HttpContext.Session.GetString("message");
            if(isAlive()){
                ViewBag.reset = false;
            }
            else{
                ViewBag.reset = true;
                message = "Your pet is DEAD";
            }

            if(win()){
                message = "You did it congrats you are the best";
                ViewBag.reset = true;
            }
            
            ViewBag.msg = message;

            return View(pet);
        }


        public IActionResult Play(){
                if(pet.Energy > 5){
                pet.Energy = pet.Energy - 5;
                int like = rand.Next(0, 101);
                int happiness = rand.Next(5, 11);
                
                if(like > 25){
                    pet.Happiness = pet.Happiness + happiness;
                    HttpContext.Session.SetString("message", "Your pet gained: "+ happiness +" happiness");
                }
                else{
                    HttpContext.Session.SetString("message", "Your pet didn't like the game!");
                }
                return RedirectToAction("Index");                
            }
            HttpContext.Session.SetString("message", "Your pet is tired has no energy you better put him to sleep");
            return RedirectToAction("Index");    
        }



        public IActionResult Feed(){
            int like = rand.Next(0, 101);
            if(pet.Meals > 0){
                pet.Meals = pet.Meals - 1;
                if(like > 25){
                    int fullness = rand.Next(5, 11);
                    pet.Fullness = pet.Fullness + fullness;
                    HttpContext.Session.SetString("message", "Pet was fed and gained: " + fullness + " fullness");
                }
                else{
                    HttpContext.Session.SetString("message", "Your pet didn't like the food");
                    return RedirectToAction("Index");
                }
            }
            else{
                HttpContext.Session.SetString("message", "You have no food left, you better go to work");
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }


        public IActionResult Work(){
            if(pet.Energy > 5){
                pet.Energy = pet.Energy - 5;
                int meals = rand.Next(1,4);
                pet.Meals = pet.Meals + meals;   
            
                HttpContext.Session.SetString("message", "You gained " + meals + " food");
                return RedirectToAction("Index");
            }
            HttpContext.Session.SetString("message", "Your pet is tired has no energy you better put him to sleep");
            return RedirectToAction("Index");            
        }

        public IActionResult Sleep(){
            pet.Fullness = pet.Fullness - 5;
            pet.Happiness = pet.Happiness - 5;
            pet.Energy = pet.Energy + 15;
            HttpContext.Session.SetString("message", "You earned 15 energy");
            return RedirectToAction("Index");
        }

        public IActionResult Reset(){
            pet = new Pet("bobic");
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

        public bool isAlive(){
            if(pet.Fullness <= 0 || pet.Happiness <= 0){
                return false;
            }
            return true;
        }

        public bool win(){
            if(pet.Energy >= 100 && pet.Happiness >= 100 && pet.Fullness >= 100){
                return true;
            }
            return false;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
