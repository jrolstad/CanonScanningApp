using System;
using Atalasoft.Twain;

namespace CanonScanningApp
{
    public class Program
    {
        private static Acquisition _acquisition;
        private static Device _device;

        private static void Main(string[] args)
        {
            _acquisition = new Acquisition();
            _acquisition.ImageAcquired += image_acquired;
            _acquisition.AcquireCanceled += acquire_canceled;
            _acquisition.AcquireFinished += acq_AcquireFinished;
            _acquisition.AsynchronousException += acq_AsynchronousException;

            _device = _acquisition.ShowSelectSource();

            try
            {
                if (_device.TryOpen())
                {
                    _device.AutoScan = true;
                    _device.HideInterface = true;
                    _device.ModalAcquire = false;
                    _device.DocumentFeeder.AutoFeed = true;

                    Console.WriteLine("Start Scanning Documents...");
                    _device.Acquire();

                    Console.Write("Press Enter when scanning is complete");
                    Console.ReadLine();

                    Console.WriteLine("Closing Scanner...");
                    _device.Close();
                    Console.WriteLine("  Closed");
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine("  -------Exception:-------");
                Console.WriteLine(exception);
            }

        }

        private static void acq_AsynchronousException(object sender, AsynchronousExceptionEventArgs e)
        {
            Console.WriteLine("  -------Acquire asynchronous exception:-------");
            Console.WriteLine(e.Exception);
        }

        private static void acq_AcquireFinished(object sender, EventArgs e)
        {
            Console.WriteLine("Acquire Finished");
        }

        private static void acquire_canceled(object sender, EventArgs e)
        {
            Console.WriteLine("Acquire Canceled");
        }

        private static void image_acquired(object sender, AcquireEventArgs e)
        {
            Console.WriteLine("Image acquired");
        }
    }



}

