namespace ASCII_Art_Generator;

class Program
{
    static void Main()
    {
        // we start by creating our convert with a custom "Ramp"
        var Converter = new AsciiConverter(" .:-=+*#%@");
        Console.WriteLine("--- Testing the Brightness Mapping ---");
        
        // then we siumlate a row of changing pixels changing from 255 (white) to 0 (black)
        for (int brightness = 255; brightness >= 0; brightness -= 15)
        {
            char result = Converter.MapBrightnessToChar(brightness);
            Console.WriteLine($"Brightness {brightness:D3} | Character: '{result}'");
        }

        Console.WriteLine("\n--- Visual Gradient Test ---");

        for (int i = 255; i >= 0; i -= 5)
            {
                Console.Write(Converter.MapBrightnessToChar(i));
            }
            
            Console.WriteLine("\n\nTest complete. Press any key to exit.");
            Console.ReadKey();
    }
}

public class AsciiConverter
{
    private readonly string _characterRamp;

    public AsciiConverter(string ramp = " .:-=+*#%@")
    {
        _characterRamp = ramp;
    }

    public char MapBrightnessToChar(int brightness)
    {
        // we clamp the value between 0 and 255
        int clampedValue = Math.Clamp(brightness, 0, 255);
        // we then map the 0-255 to 0 into -> (RampLength - 1)
        int index = clampedValue * (_characterRamp.Length - 1) /255;
        return _characterRamp[index];
    }
}
