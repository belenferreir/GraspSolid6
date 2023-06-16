//-------------------------------------------------------------------------
// <copyright file="Recipe.cs" company="Universidad Católica del Uruguay">
// Copyright (c) Programación II. Derechos reservados.
// </copyright>
//-------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace Full_GRASP_And_SOLID
{
    public class Recipe : IRecipeContent // Modificado por DIP
    {
        // Cambiado por OCP
        private IList<BaseStep> steps = new List<BaseStep>();

        public Product FinalProduct { get; set; }

        // Agregado por Creator
        public void AddStep(Product input, double quantity, Equipment equipment, int time)
        {
            Step step = new Step(input, quantity, equipment, time);
            this.steps.Add(step);
        }

        // Agregado por OCP y Creator
        public void AddStep(string description, int time)
        {
            WaitStep step = new WaitStep(description, time);
            this.steps.Add(step);
        }

        public void RemoveStep(BaseStep step)
        {
            this.steps.Remove(step);
        }

        // Agregado por SRP
        public string GetTextToPrint()
        {
            string result = $"Receta de {this.FinalProduct.Description}:\n";
            foreach (BaseStep step in this.steps)
            {
                result = result + step.GetTextToPrint() + "\n";
            }

            // Agregado por Expert
            result = result + $"Costo de producción: {this.GetProductionCost()}";

            return result;
        }

        // Agregado por Expert
        public double GetProductionCost()
        {
            double result = 0;

            foreach (BaseStep step in this.steps)
            {
                result = result + step.GetStepCost();
            }

            return result;
        }

        private bool cooked = false;
        public bool Cooked 
        {
            get 
            {
                return cooked;
            }
        }
        
        public int GetCookTime()
        {
            int totalTime = 0;
            foreach (BaseStep step in this.steps)
            {
                totalTime = totalTime + step.Time;
            }
            return totalTime;
        }

        public void GetCooked()
        {
            this.cooked = true;
        }
        public void Cook()
        {
            if (Cooked == false)
            {
                RecipeTimer timer = new RecipeTimer(this);
                timer.RegisterTimer();
            }
        }
    }
}

//Single Responsibility Principle: La clase tiene una única responsabilidad, la cual es representar una receta.

//Expert: Los métodos GetCookTime() y GetCooked() tiene el conocimiento necesario para obtener información sobre el tiempo y estado de la cocción.

//Encapsulamiento: La variable cooked está oculta en una propiedad de solo lectura Cooked, y su estado se modifica a través del método GetCooked()
