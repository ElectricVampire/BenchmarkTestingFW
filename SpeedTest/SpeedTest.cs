using System;
using System.Linq;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace SpeedTest
{
    /// <summary>
    /// Any type which contains methods which need to be benchmarked should be decorated with "SpeedTest" class
    /// </summary>
    public class SpeedTest
    {

        /// <summary>
        /// Execute all the methods of all the assemblies present in same directory of ExecuteTestCase.exe and decorated with appropriate "SpeedTest" attributes
        /// </summary>
        public static void ExecuteDecoratedTestCases()
        {
            string[] assemblies = GetCurrentDirAsms();

            foreach (string asm in assemblies)
            {
                RunSpeedTestOnDecoratedMethods(asm);
            }

            Console.WriteLine("Tests Completed.");
        }

        /// <summary>
        /// Execute all the methods which are decorated with "SpeedTest" class and "SpeedTestMethodAttribute" type
        /// </summary>
        /// <param name="asm"> Fully qualified Assembly name</param>
        private static void RunSpeedTestOnDecoratedMethods(string asm)
        {
            // Load asm
            var assembly = Assembly.LoadFile(asm);

            //Get all the types
            var AsmTypes = assembly.GetTypes();

            foreach (var type in AsmTypes)
            {
                // Check if it is decorated with SpeedTest class
                if (IsClassDecoratedWithSpeedTest(type))
                {

                    object instance = Activator.CreateInstance(type);
                    var methods = type.GetMethods();

                    foreach (var method in methods)
                    {
                        var tempMethod = method;
                        SpeedTestMethodAttribute methodAttrib = GetSpeedTestMethodAttribute(tempMethod);
                        if (methodAttrib != null)
                        {
                            string displayMsg = string.Format("Method Name: {0}{1}{2}", tempMethod.Name, Environment.NewLine, methodAttrib.Message);

                            // Run Init method of SpeedTest method
                            if (methods.Any(x => string.Equals(x.Name, methodAttrib.InitializationMethod)))
                            {
                                type.GetMethod(methodAttrib.InitializationMethod).Invoke(instance, null);
                            }

                            TestSpeed(displayMsg, methodAttrib.NoOfIterations, () => tempMethod.Invoke(instance, null));

                            //Run Reset method of SpeedTest method
                            if (methods.Any(x => string.Equals(x.Name, methodAttrib.ResetMethod)))
                            {
                                type.GetMethod(methodAttrib.ResetMethod).Invoke(instance, null);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Get all the attributes of decorated method
        /// </summary>
        /// <param name="method">Method for which attributes need to be retrieved</param>
        /// <returns>All the attributes of decorated method</returns>
        private static SpeedTestMethodAttribute GetSpeedTestMethodAttribute(MethodInfo method)
        {
            SpeedTestMethodAttribute methodAttribute = null;
            foreach (var attribute in method.GetCustomAttributes(true))
            {
                if (attribute is SpeedTestMethodAttribute)
                {
                    methodAttribute = attribute as SpeedTestMethodAttribute;
                    break;
                }
            }
            return methodAttribute;
        }

        /// <summary>
        /// Check if the type is decorated with SpeedTest class
        /// </summary>
        /// <param name="type">Type which need to be checked</param>
        /// <returns>true if the type is decorated with SpeedTest class otherwise false</returns>
        private static bool IsClassDecoratedWithSpeedTest(Type type)
        {
            bool returnCode = false;
            foreach (var attribute in type.GetCustomAttributes(true))
            {
                returnCode = attribute is SpeedTestClassAttribute;
                break;
            }
            return returnCode;
        }

        /// <summary>
        /// Get all the assemblies in directory of ExecuteTestCase.exe
        /// </summary>
        /// <returns>string array containing fully qualified names of all the assemblies</returns>
        private static string[] GetCurrentDirAsms()
        {
            return Directory.GetFiles(Directory.GetCurrentDirectory(), "*.dll");
        }

        /// <summary>
        /// Test and display the execution speed of method
        /// </summary>
        /// <param name="msg">Message which need to be displayed on console for given test method</param>
        /// <param name="noOfIterations">Number of times method need to be executed</param>
        /// <param name="action">method name or any other delegate which need to be executed for execution speed calculation</param>
        private static void TestSpeed(string msg, int noOfIterations, Action action)
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            Stopwatch stopWatch = Stopwatch.StartNew();
            for (int i = 0; i < noOfIterations; i++)
            {
                action();
            }
            stopWatch.Stop();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(msg);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Time consumed is " + stopWatch.ElapsedMilliseconds + "ms");
            Console.ResetColor();
            Console.WriteLine("==========================================================================================================");
        }
    }
}
