using System.IO;
using System.Media;
using System;

namespace Prog6221_Part1
{
    public class play_sound
    {
        //this is a consructor
        public play_sound()
        {
            //gettting the app full location
            string full_location = AppDomain.CurrentDomain.BaseDirectory;
            Console.WriteLine(full_location);

            //replace the bin\debug\folders
            string new_location = full_location.Replace("bin\\Debug\\", "");
            Console.WriteLine(new_location);
            //combine both the new location and audio file
            string full_path = Path.Combine(new_location, "voice_greeting.wav");

            // try and catch, error handling
            try
            {
                //use using funtion to create play instance
                // Create a new SoundPlayer instance and play the sound

                using (SoundPlayer player = new SoundPlayer(full_path))

                    player.PlaySync();
            } // End of using block


            //end of using function
            catch (Exception error)
            {

                Console.WriteLine("Error: " + error.Message);

            }//end of catch

        }// end of constructor

    }//end of class
}// end of namespace
