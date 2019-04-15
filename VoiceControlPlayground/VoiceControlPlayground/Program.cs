using System;
using System.Globalization;
using System.Speech.Recognition;
using System.Threading.Tasks;
using CineNet.Common.Extensions;

namespace VoiceControlPlayground
{
	class Program
	{
		private static VoiceControlProcessor processor;

		private static SpeechRecognitionEngine speechRecognitionEngine;

		private static void BuildGrammar()
		{
			var choices = new Choices();

			choices.Add("v p n");
			choices.Add("pull all repos");
			choices.Add("m s teams");

			var grammar = new Grammar(choices);

			speechRecognitionEngine.LoadGrammar(grammar);
		}

		static void Main(string[] args)
		{
			ConsoleExtensions.WriteLineWithColor(ConsoleColor.Yellow, "Starting Speech Engine.");

			processor = new VoiceControlProcessor();

			Task.Run(() => StartSpeechEngine());

			Console.ReadKey(true);

			speechRecognitionEngine?.Dispose();
		}

		static void OnSpeechRecognized(object sender, SpeechRecognizedEventArgs e)
		{
			ConsoleExtensions.WriteLineWithColor(ConsoleColor.Cyan, $"{e.Result.Text} | Confidence = {e.Result.Confidence}");

			if (e.Result.Confidence < .91) return;

			processor.ProcessRequest(e.Result.Text);
		}

		private static void StartSpeechEngine()
		{
			// Create an in-process speech recognizer for the en-US locale.
			try
			{
				speechRecognitionEngine = new SpeechRecognitionEngine(new CultureInfo("en-US"));

				BuildGrammar();

				// Add a handler for the speech recognized event.
				speechRecognitionEngine.SpeechRecognized += OnSpeechRecognized;

				//recognizer.AudioLevelUpdated += OnAudioLevelUpdated;

				// Configure input to the speech recognizer.
				speechRecognitionEngine.SetInputToDefaultAudioDevice();

				// Start asynchronous, continuous speech recognition.
				speechRecognitionEngine.RecognizeAsync(RecognizeMode.Multiple);
			}
			catch (Exception ex)
			{
				ex.PrintToConsole();
			}
		}
	}
}