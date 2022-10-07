using System;
using System.Collections.Generic;

namespace paintV3
{

    public class materialList
    {

        //defualt rules for new objects that dont have specified behavior
        List<rule> NoRules = new List<rule>();

        public List<material> mat_list = new List<material>();

        public void Add( material m) {
            mat_list.Add(m);
        }

        public materialList() {


        }

    }


    public class material //parent material template
    {

        public string name = "null";
        public int variation = 0;
        public int colorR = 255;//primary color values
        public int colorG = 0;//primary color values
        public int colorB = 0;//primary color values
        public bool transparent = false;
        public bool peekOverlap = true;
        public bool fired = false;

        public bool acted = false;
        public List<rule> rules = new List<rule> { };
        public material(){}
        public material(string name_, int colorR_, int colorG_, int colorB_, int variation_, bool transperent_, bool peekOverlap_, List<rule> rules_)
        {
            name = name_;
            variation = variation_;
            colorR = colorR_;
            colorG = colorG_;
            colorB = colorB_;
            transparent = transperent_;
            peekOverlap = peekOverlap_;
            rules = rules_;
        }

        public virtual material clone() 
        {
            material newMat = new material();

            newMat.name = name;
            newMat.variation = variation;
            newMat.colorR = colorR;
            newMat.colorG = colorG;
            newMat.colorB = colorB;
            newMat.transparent = transparent;
            newMat.peekOverlap = peekOverlap;
            newMat.fired = fired;
            newMat.rules = rules;

            return newMat; 
        
        }
    }

    public unsafe class rule {
        public bool condition;
        public int conditionX;
        public int conditionY;
        public string name;
        public bool fired;
        public bool* ScriptNum;

        public unsafe rule(bool condition_, int conditionX_, int conditionY_, string name_,bool* num)
        {
            condition = condition_;
            conditionX = conditionX_;
            conditionY = conditionY_;
            name = name_;
        }

        public rule(bool condition_, int conditionX_, int conditionY_, string name_) 
        { 
            condition = condition_;
            conditionX = conditionX_;
            conditionY = conditionY_;
            name = name_;
        }
    }
}