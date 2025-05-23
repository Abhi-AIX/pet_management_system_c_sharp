using System.Data.Common;
using System.Runtime.CompilerServices;
using System.Transactions;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("🐾 Welcome to Happy Paws Animal Shelter 🐾\nYour virtual pet shelter assistant");



        bool exit = false;
        int menu = 0;

        string[] ourAnimals = new string[10];
        string[] labels = { "ID #:", "Species:", "Age:", "Physical description:", "Personality:", "Nickname:" };

        ourAnimals[0] = "D001,dog,3,Small brown male,Playful,Rocky";
        ourAnimals[1] = "C001,cat,2,White fluffy female,Shy,Whiskers";
        ourAnimals[2] = "D002,dog,5,Large black male,Loyal,Bruno";
        ourAnimals[3] = "C002,cat,1,Grey short-haired female,Curious,Luna";
        ourAnimals[4] = "D003,dog,4,Golden retriever male,Friendly,Charlie";
        ourAnimals[5] = "C003,cat,3,Orange tabby male,Energetic,Simba";
        ourAnimals[6] = "D004,dog,6,Tiny chihuahua female,Alert,Bella";
        ourAnimals[7] = "C004,cat,7,Black long-haired male,Independent,Shadow";


        do
        {
            Console.WriteLine("\nMain Menu:\n 1. View all animals\n 2. Add new animal\n 3. Edit an Animal’s Age \n 4. Edit an Animal’s Personality \n 5. Search animals by physical characteristic\n 6. case 6: Validate and Fix Incomplete Animal Data \n 7.Type exit to quit");
            string input = Console.ReadLine();

            if (int.TryParse(input, out menu))
            {

                switch (menu)
                {
                    case 1:

                        foreach (string a in ourAnimals)
                        {
                            if (!string.IsNullOrWhiteSpace(a))
                            {
                                Console.WriteLine(a);
                            }
                        }

                        break;


                    case 2:

                        Console.WriteLine("Add new Animal: ");

                        Random random = new Random();

                        bool insertElement = false;
                        int number = random.Next(0, 10);
                        string idPrefix = "";

                        Console.WriteLine("Enter the Animal Specie:");
                        string speciesDogOrCat = Console.ReadLine();

                        Console.WriteLine("Enter the Animal age:");
                        int age = Convert.ToInt32(Console.ReadLine());

                        Console.WriteLine("Enter the Animal physical Dexcription:");
                        string physicalDescription = Console.ReadLine();

                        Console.WriteLine("Enter the Animal personality:");
                        string personality = Console.ReadLine();

                        Console.WriteLine("Enter the Animal nickname:");
                        string nickname = Console.ReadLine();


                        if (speciesDogOrCat.Equals("Dog", StringComparison.OrdinalIgnoreCase))
                        {
                            idPrefix = "D";
                        }
                        else if (speciesDogOrCat.Equals("Cat", StringComparison.OrdinalIgnoreCase))
                        {
                            idPrefix = "C";
                        }
                        else
                        {
                            Console.WriteLine("Enable to generate id please select valid animal");
                        }


                        bool isAdded = false;

                        for (int i = 0; i < ourAnimals.Length; i++)
                        {

                            if (string.IsNullOrWhiteSpace(ourAnimals[i]))
                            {
                                string id = $"{idPrefix}00{i + 1}";

                                ourAnimals[i] = $"{id}, {speciesDogOrCat}, {age}, {physicalDescription}, {personality}, {nickname}";
                                isAdded = true;
                                Console.WriteLine("Animal Added sucessfully");
                                break;
                            }

                        }
                        if (!isAdded)
                        {
                            Console.WriteLine("Sorry we can not add more pets for now.");
                        }


                        break;

                    case 3:

                        Console.WriteLine("Please Enter Animal ID: ");
                        string enterAnimalId = Console.ReadLine().Trim();

                        bool found = false;

                        for (int i = 0; i < ourAnimals.Length; i++)
                        {
                            if (!string.IsNullOrWhiteSpace((ourAnimals[i])) && ourAnimals[i].StartsWith(enterAnimalId))
                            {
                                string[] petData = ourAnimals[i].Split(",");
                                Console.WriteLine($"Current age of the pet is {petData[2]}");
                                Console.WriteLine("Enter New Age");

                                int newAge = Convert.ToInt32(Console.ReadLine());

                                petData[2] = newAge.ToString();

                                ourAnimals[i] = string.Join(",", petData);

                                Console.WriteLine("Succesfully updated age.");


                                //If we are trying to update particular value are performing such an operation in for loop on particular value do not 
                                //forget to break the loop if not it will repeat again
                                found = true;
                                break;

                            }

                        }

                        if (!found)
                        {
                            Console.WriteLine("Can't find any pet with provided id");
                        }

                        break;

                    case 4:
                        // Animal’s Personality 

                        Console.WriteLine("Enter the Animal ID: ");
                        string enterId = Console.ReadLine().Trim();

                        bool isFound = false;

                        for (int i = 0; i < ourAnimals.Length; i++)
                        {
                            if (!string.IsNullOrWhiteSpace(ourAnimals[i]) && ourAnimals[i].StartsWith(enterId))
                            {
                                string[] petData = ourAnimals[i].Split(',');

                                Console.WriteLine($"Current personality of the pet: {petData[4]}");
                                Console.WriteLine("Please enter the new personality: ");

                                string newPersonality = Console.ReadLine().Trim();

                                petData[4] = newPersonality.ToString();

                                ourAnimals[i] = string.Join(",", petData);

                                Console.WriteLine("Successfully updated personlaity");

                                isFound = true;
                                break;
                            }
                        }

                        if (!isFound)
                        {
                            Console.WriteLine("Can't find any pet with provided id");
                        }

                        break;

                    default:

                        Console.WriteLine("please select valid option.");

                        break;

                    case 5:

                        Console.Write("Enter the species to search (dog/cat): ");
                        string searchSpecies = Console.ReadLine().Trim().ToLower();

                        Console.WriteLine("Enter a keyword to search in physical description:");
                        string keyword = Console.ReadLine().Trim().ToLower();

                        bool anyMatch = false;

                        for (int i = 0; i < ourAnimals.Length; i++)
                        {
                            if (!string.IsNullOrWhiteSpace(ourAnimals[i]))
                            {
                                string[] petData = ourAnimals[i].Split(',');

                                string species = petData[1].Trim().ToLower();
                                string searchphysicalDesc = petData[3].Trim().ToLower();


                                if (species.Equals(searchSpecies) && searchphysicalDesc.Contains(keyword))
                                {
                                    for (int j = 0; j < petData.Length; j++)
                                    {
                                        Console.WriteLine($"{labels[j]} {petData[j].Trim()}");
                                    }
                                    Console.WriteLine(); // spacing between pets
                                    anyMatch = true;

                                    // ourAnimals[i] = string.Join(",", petData);
                                    // Console.WriteLine($"{labels[j]}: {ourAnimals[i]}");
                                    // anyMatch = true;
                                    // break;
                                }


                            }
                        }

                        if (!anyMatch)
                        {
                            Console.WriteLine("None found.");
                        }


                        break;


                    case 6:

                        Console.WriteLine("All missing/incomplete ages, physical descriptions and Personality are now valid.");
                        Console.WriteLine("Press Enter to continue...");
                        Console.ReadLine();

                        for (int i = 0; i < ourAnimals.Length; i++)
                        {
                            if (!string.IsNullOrWhiteSpace(ourAnimals[i]))
                            {
                                string[] petData = ourAnimals[i].Split(',');
                                
                                if (string.IsNullOrWhiteSpace(petData[2]) || petData[2].Trim() == "?")
                                {
                                    Console.WriteLine($"\nAnimal with ID {petData[0]} has missing/invalid age.");
                                    Console.Write("Enter a valid age: ");

                                    string inputAge = Console.ReadLine();
                                    bool isValid = int.TryParse(inputAge, out int validAge);

                                    while (!isValid)
                                    {
                                        Console.Write("Invalid age. Please enter a number: ");
                                        inputAge = Console.ReadLine();
                                        isValid = int.TryParse(inputAge, out validAge);
                                    }
                                   
                                    petData[2] = validAge.ToString();
                                    ourAnimals[i] = string.Join(",", petData);

                                    Console.WriteLine($"Age updated for pet {petData[0]}.\n");
                                }

                                //validate physical description

                                if (string.IsNullOrWhiteSpace(petData[3]) || petData[3].Trim() == "?")
                                {
                                    Console.WriteLine($"\nAnimal with ID {petData[0]} has missing/invalid physical dexcription.");
                                    Console.Write("Enter a valid physical description: ");

                                    string inputNewPhysiscaldesc = Console.ReadLine();

                                    while (string.IsNullOrWhiteSpace(inputNewPhysiscaldesc))
                                    {
                                        Console.Write("Invalid physical description. Please enter a valid description: ");
                                        inputNewPhysiscaldesc = Console.ReadLine();
                                    }

                                    petData[3] = inputNewPhysiscaldesc;
                                    ourAnimals[i] = string.Join(",", petData);
                                    Console.WriteLine($"Physical description updated for pet {petData[0]}.\n");
                                    
                                }

                                if(string.IsNullOrWhiteSpace(petData[4]) || petData[4].Trim() == "?")
                                {
                                    Console.WriteLine($"\nAnimal with ID {petData[0]} has missing/invalid personality.");
                                    Console.Write("Enter a valid personality: ");

                                    string inputNewPersonality = Console.ReadLine();

                                    while (string.IsNullOrWhiteSpace(inputNewPersonality))
                                    {
                                        Console.Write("Invalid personality. Please enter a valid personality: ");
                                        inputNewPersonality = Console.ReadLine();
                                    }

                                    petData[4] = inputNewPersonality;
                                    ourAnimals[i] = string.Join(",", petData);
                                    Console.WriteLine($"Personality updated for pet {petData[0]}.\n");
                                }
                            }
                        }

                        break;

                }


            }
            else if (input.Equals("exit", StringComparison.OrdinalIgnoreCase))
            {
                exit = true;
            }
            else
            {
                Console.WriteLine("Please provide a valid input.");
            }


        } while (!exit);

    }
}

