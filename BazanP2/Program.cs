using System;
using System.IO;

namespace BazanP2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            double mass;
            string sMas;
            double pressureP; //pressure
            double volume; // Voulume in cubic meters
            string sVolume;
            //const double R = 8.3145 is a constant.
            double tempC; //temp in Cesius 
            string sTempC;
            string[] Gnames = new string[85];
            double[] Mweights = new double[85];
            string choice;
            int count = 0;
            double moleweight = 0;
            string response;

            //IdealGas gas = new IdealGas();
            try
            {

                do
                {
                    IdealGas gas = new IdealGas();
                    GetMolecularWeights(ref Gnames, ref Mweights, out count);
                    DisplayGasNames(Gnames, count);
                    do // This do loop is to make sure that the uses enters an element on the table.
                    {
                        Console.Out.WriteLine("\nPlease enter in a gas displayed above (Case sensetive): ");
                        choice = Console.ReadLine();
                        moleweight = GetMolecularWeightFromName(choice, Gnames, Mweights, count);
                        gas.SetMolecularWeight(moleweight);
                    } while (moleweight == 0);
                    try
                    {
                        Console.Out.WriteLine("Please enter the mass of the gas in grams: ");
                        sMas = Console.ReadLine();
                        mass = double.Parse(sMas);
                        gas.SetMass(mass);
                        Console.Out.WriteLine("Please enter in the volume of gas in cubic meters: ");
                        sVolume = Console.ReadLine();
                        volume = double.Parse(sVolume);
                        gas.SetVolume(volume);
                        Console.Out.WriteLine("Please enter in the temperature in degrees Celsius  ");
                        sTempC = Console.ReadLine();
                        tempC = double.Parse(sTempC);
                        gas.SetTemp(tempC);
                    }
                    catch (FormatException exe)
                    {
                        Console.WriteLine("Error: " + exe.Message);

                    }
                    //pressureP = Pressure(mass, volume, tempC, moleweight);
                    try
                    {
                        gas.GetPressure();
                    }
                    catch (OverflowException exc)
                    {
                        Console.WriteLine("Error: " + exc.Message);
                    }
                    DisplayPressure(gas.GetPressure());

                    Console.Out.WriteLine("Would you like to do another yes or now?"); //Will prompt the user for if they want to exit or do another.
                    response = Console.ReadLine();
                } while (response == "yes");
            }
            catch (Exception exc)
            {
                Console.WriteLine("Error: " + exc.Message);
            }

            Console.Out.WriteLine("Thanks for using the program, GoodBye!");


        }

        static void GetMolecularWeights(ref string[] gasNames, ref double[] molecularWeights, out int count)
        {

            count = 0;
            gasNames = new string[85];
            molecularWeights = new double[85];
            //GetMolecularWeights(ref Gnames, ref Mweights, out count);

            string[] readText = File.ReadAllLines(@"C:\Users\micha\Desktop\C#\BazanP1\BazanP1\MWGV.csv");
            // file. read all lines reads in the csv file.
            for (count = 1; count < readText.Length; ++count) // we assign i to one so that we can ignore the text on line 1 in the csv file.
            {

                string[] split_Element;
                split_Element = readText[count].Split(','); // we assign new array to split element so that we can use the split function.
                // this makes is so that the elements in each line of split_elemnt become assigned to a index of either 0 or 1;
                gasNames[count - 1] = split_Element[0];

                string temp = split_Element[1];
                molecularWeights[count - 1] = double.Parse(temp);
                //we have to parse the array so that the elements become doubles within the array.

            }

        }

        private static void DisplayGasNames(string[] gasNames, int countGases)
        {
            string hello = "\t\t\t\tWelcome to the Ideal Gas Calculator.";

            Console.Out.WriteLine(hello);

            for (int i = 0; i < gasNames.Length; i++)
            {

                if (i % 3 == 0)
                {
                    Console.WriteLine();
                }
                // if the index is divisable by 3 it creates a new lines.
                string list = String.Format("{0,-30}", gasNames[i]);
                // this is a output form modifier to make the spacing even.
                Console.Write(list);
            }
        }

        public static double GetMolecularWeightFromName(string gasName, string[] gasNames,
         double[] molecularWeights, int countGases)
        {
            double moleweight;
            int index = 0;


            //int index = Array.IndexOf(gasNames, gasName);            

            for (int i = 0; i < gasNames.Length; ++i)

            {
                if (gasNames[i] == gasName)
                {
                    index = i;

                }

            }
            if (gasNames[index] == gasName)
            {

                Console.Out.WriteLine("The element {0} has a molecular weight of {1}\n", gasName, molecularWeights[index]);
                moleweight = molecularWeights[index];
                IdealGas gas = new IdealGas();
                return moleweight;
            }

            else
            {
                Console.Out.WriteLine("\n{0} is not found on the list!", gasName);
                moleweight = 0;
                return moleweight;
                // if mole weights is = to 0 it restarts the loop.
            }

        }
        static double CelciusToKelvin(double celcius)
        {
            double kelvin;

            kelvin = celcius + 273.15;
            // the conversion from c to k
            return kelvin;
        }

        static double NumberOfMoles(double mass, double molecularWeight)
        {
            double moles;
            moles = mass / molecularWeight;

            return moles;
        }

        static double Pressure(double mass, double vol, double temp, double molecularWeight)
        {
            const double R = 8.3145; // R is a constant.
            double T;
            double n;
            double P; // will be returned.
            n = NumberOfMoles(mass, molecularWeight);
            T = CelciusToKelvin(temp);

            P = (n * R * T) / vol;

            return P;
        }

        static double PaToPSI(double pascals)
        {
            double PSI;

            PSI = pascals / 6895;


            return PSI;
        }

        private static void DisplayPressure(double pressure)
        {
            double PSI;
            PSI = PaToPSI(pressure);
            Console.Out.WriteLine("The Pressure in Pascals is: " + pressure);
            Console.Out.WriteLine("The Pressure in PSI is: " + PSI);
            //displays Pascals and psi to the user.
        }



    }


    public class IdealGas
    {
        private double mass;
        private double volume;
        private double temp;
        private double mWeight;
        private double pressure;




        public void SetMass(double value)
        {
            //This indicated is will be instantiated into the instance variable value of whatever follows this.
            this.mass = value;
        }

        public double GetMass()
        {
            return mass;
        }

        public void SetVolume(double value)
        {
            this.volume = value;
        }

        public double GetVolume()
        {
            return volume;
        }

        public void SetTemp(double value)
        {
            this.temp = value;
        }

        public double GetTemp()
        {
            return temp;
        }

        public void SetMolecularWeight(double value)
        {
            this.mWeight = value;
        }

        public double GetMolecularWeight()
        {
            return mWeight;
        }

        public double GetPressure()
        {
            //Calc has to be called in this GetPressure method because it is set to private so it cannot be called in the main function.
            Calc();
            return pressure;
        }


        private void Calc()
        {

            const double R = 8.3145; // R is a constant.
            double T;
            double n;

            n = this.mass / this.mWeight;
            T = this.temp + 273.15;

            this.pressure = (n * R * T) / volume;
        }






    }

}




