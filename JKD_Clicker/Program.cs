using CommandLine;
using JKD_Clicker;

Console.WriteLine("JKD!");
Parser.Default.ParseArguments<Options>(args)
                  .WithParsed(o =>
                  {
                      JKDPoll test = new JKDPoll(o);
                      test.jKDPoll();
                  });

