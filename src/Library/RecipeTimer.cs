using System;

namespace Full_GRASP_And_SOLID
{
    public class RecipeTimer : TimerClient
    {
        public Recipe Recipe { get; set; }
        public RecipeTimer(Recipe recipe)
        {
            this.Recipe = recipe;
        }
        public void RegisterTimer()
        {
            CountdownTimer timer = new CountdownTimer();
            timer.Register(this.Recipe.GetCookTime(), this);
        }
        public void TimeOut()
        {
            this.Recipe.GetCooked();
        }
    }
}

//Single Responsibility Principle: La clase tiene una única responsabilidad, la cual es
//establecer un temporizador para la receta y registrar la finalización de la cocción.