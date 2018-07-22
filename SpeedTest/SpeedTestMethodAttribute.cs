using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeedTest
{
    /// <summary>
    /// Method for which the execution speed need to be calculated should be decorated with SpeedTestMethodAttribute and under ctor various attribute can be passed
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class SpeedTestMethodAttribute : Attribute
    {
        private const int DEFAULT_ITERATIONS = 1;

        /// <summary>
        /// Number of times method should be executed during time calculation
        /// Default value 1
        /// </summary>
        public int NoOfIterations { get; set; } = DEFAULT_ITERATIONS;

        /// <summary>
        /// Message which need to be displayed on console after test method execution
        /// </summary>
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// This method will be executed before executing the speed test
        /// Generally this method should contain all the task which need to be performed before method execution
        /// </summary>
        public string InitializationMethod { get; set; } = null;

        /// <summary>
        /// This method will be executed after speed test case execution
        /// Generally this method should clear all the data to avoid any impact on other speed test methods
        /// </summary>
        public string ResetMethod { get; set; } = null;

        /// <summary>
        /// Method decorated with SpeedTestMethodAttribute will be executed for speed test calculation with following defaults
        /// - No of iterations : 1
        /// - Display message  : None
        /// - Initialization method : None
        /// - Reset method : None
        /// </summary>
        public SpeedTestMethodAttribute() : this(DEFAULT_ITERATIONS)
        {

        }

        /// <summary>
        /// Method decorated with SpeedTestMethodAttribute will be executed for speed test calculation
        /// </summary>
        /// <param name="itrations">Number of times method should be executed during time calculation. Default value 1</param>
        public SpeedTestMethodAttribute(int itrations) : this(itrations, string.Empty)
        {

        }

        /// <summary>
        /// Method decorated with SpeedTestMethodAttribute will be executed for speed test calculation
        /// </summary>
        /// <param name="itrations">Number of times method should be executed during time calculation. Default value 1</param>
        /// <param name="message">Message which need to be displayed on console after test method execution</param>
        public SpeedTestMethodAttribute(int itrations, string msg) : this(itrations, msg, null)
        {

        }

        /// <summary>
        /// Method decorated with SpeedTestMethodAttribute will be executed for speed test calculation
        /// </summary>
        /// <param name="itrations">Number of times method should be executed during time calculation. Default value 1</param>
        /// <param name="message">Message which need to be displayed on console after test method execution</param>
        /// <param name="initMethod">This method will be executed before executing the speed test. Generally this method should contain all the task which need to be performed before method execution</param>        
        public SpeedTestMethodAttribute(int itrations, string msg, string initializationMethod) : this(itrations, msg, initializationMethod, null)
        {

        }

        /// <summary>
        /// Method decorated with SpeedTestMethodAttribute will be executed for speed test calculation
        /// </summary>
        /// <param name="itrations">Number of times method should be executed during time calculation. Default value 1</param>
        /// <param name="message">Message which need to be displayed on console after test method execution</param>
        /// <param name="initMethod">This method will be executed before executing the speed test. Generally this method should contain all the task which need to be performed before method execution</param>
        /// <param name="resetMethod">This method will be executed after speed test case execution.Generally this method should clear all the data to avoid any impact on other speed test methods</param>
        public SpeedTestMethodAttribute(int itrations, string message, string initMethod, string resetMethod)
        {
            NoOfIterations = itrations;
            Message = message;
            InitializationMethod = initMethod;
            ResetMethod = resetMethod;
        }
    }
}
