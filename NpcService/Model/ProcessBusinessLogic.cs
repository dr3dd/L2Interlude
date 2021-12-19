using System;

namespace NpcService.Model
{
    public class ProcessBusinessLogic
    {
        // declaring an event using built-in EventHandler
        public event EventHandler<ProcessEventArgs> ProcessCompleted; 

        public void StartProcess()
        {
            var data = new ProcessEventArgs();
		
            try
            {
                Console.WriteLine("Process Started!");
			
                // some code here..
            
                data.IsSuccessful = true;
                data.CompletionTime = DateTime.Now;
                OnProcessCompleted(data);
            }
            catch(Exception ex)
            {
                data.IsSuccessful = false;
                data.CompletionTime = DateTime.Now;
                OnProcessCompleted(data);
            }
        }

        protected virtual void OnProcessCompleted(ProcessEventArgs e)
        {
            ProcessCompleted?.Invoke(this, e);
        }
    }
}