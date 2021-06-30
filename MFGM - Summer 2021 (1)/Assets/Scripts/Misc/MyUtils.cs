using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Enum = System.Enum;
using Random = UnityEngine.Random;

/// <summary>
/// Collections of my Custom Built Functions and Utilities to best suit my experience
/// </summary>
public class MyUtils : MonoBehaviour
{

    /// <summary>
    /// Instantiates the given gameObject and set its parent which is null by default
    /// </summary>
    /// <param name="gameObject">Game Object to instantiate</param>
    /// <param name="parent">Parent where the newly created Game Object will be placed in the hierarchy</param>
    /// <returns></returns>
    public static GameObject Instance(GameObject gameObject, GameObject parent=null)
    {
        GameObject newObject = Instantiate(gameObject);
        if (parent != null)
            newObject.transform.SetParent(parent.transform);
        return newObject;
    }

    /// <summary>
    /// Prints the given inputs to the debug console. Prints "unsupported" if the type is not yet implemented. The parameter
    /// input is by default Generic is dynamic in this overload.
    /// </summary>
    /// <param name="input">Dynamic arrays</param>
    public static void Print(params dynamic[] input)
    {
        Print<dynamic>(input);
    }

    /// <summary>
    /// Prints the given Generic "T" input array to the debug console. Prints "unsupported" if the type is not yet implemented.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="input"></param>
    public static void Print<T>(params T[] input)
    {
        string output = "";
            foreach(T i in input)
            {
                string toAdd = "";
                if (i is int || i is float || i is string || i is bool || i is Vector2 || i is Vector3 || i is Enum)
                {
                    toAdd = i.ToString();
                } else if(i is null)
            {
                toAdd = "null";
            }
                    else
                {
                    toAdd = typeof(T).ToString();
                }
                     output += $" {toAdd} ";
            }
        print(output);
    }


    /// <summary>
    /// Prints all the items in the given array in one single Debug.Log
    /// </summary>
    public static void PrintArray<T>(T[] inputArray)
    {
        string output = "";
        foreach(T i in inputArray)
        {
            output += $" {i} ";
        }
    }
    public static void PrintArray<T>(List<T> inputArray)
    {
        string output = "";
        for(int i = 0; i < inputArray.Count; i++)
        {
            output += $" {inputArray[i]} ";
        }
    }


    // --------------------------------------------------- UTILS ---------------------------------------------------
    /// <summary>
    /// Collection of functions that can be useful on arrays
    /// </summary>

    public class ArrayUtils
    {
        /// <summary>
        /// Picks a random 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <returns></returns>
        public static T Pick<T>(T[] array)
        {
            int index = Random.Range(0, array.Length);
            return array[index];
        }
    }
    /// <summary>
    /// Collection of functions that can be useful when using Vectors. Includes both Vector2 and Vector3
    /// </summary>
    public class VectorUtils
    {
        // ================================================= VECTOR 2 =================================================
        /// <summary>
        /// Creates a new Vector2 by using the given x and y values of the given Vector3 input
        /// </summary>
        /// <returns>A new Vector3</returns>
        public static Vector2 Switch(Vector3 input)
        {
            return new Vector2(input.x, input.y);
        }

        /// <summary>
        /// Creates a new Vector3 by using the given x and y values of the given Vector2 input and a z value which is 0 by default
        /// </summary>
        /// <returns>A new Vector2</returns>
        public static Vector3 Switch(Vector2 input, float zValue=0f)
        {
            return new Vector3(input.x, input.y, zValue);
        }

        /// <summary>
        /// Returns the angle of the given Vector2 target in floats using the Mathf.Rad2Deg
        /// </summary>
        /// <param name="target">The Vector2 which to get the angle from</param>
        /// <returns></returns>
        public static float GetAngle(Vector2 target)
        {
            return Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg;
        }

        /// <summary>
        /// Returns the angle of the given Vector2 target in radians
        /// </summary>
        /// <param name="target">The Vector2 which to get the angle from</param>
        /// <returns></returns>
        public static float GetRadianAngle(Vector2 target)
        {
            return Mathf.Atan2(target.y, target.x);
        }

        public static Vector2 Rotate(Vector2 v, float delta)
        {
            return new Vector2(
                v.x * Mathf.Cos(delta) - v.y * Mathf.Sin(delta),
                v.x * Mathf.Sin(delta) + v.y * Mathf.Cos(delta)
            );
        }

        public static Vector2 Sign(Vector2 input)
        {
            float x = Mathf.Sign(input.x);
            float y = Mathf.Sign(input.y);
            return new Vector2(x, y);
        }

        // ================================================= VECTOR 3 =================================================

        /// <summary>
        /// Creates a new Vector3 based on the given Input Vector and given x, y, z values which are 0 by default.
        /// </summary>
        /// <param name="inputVector">Vector where the whole function is based on. If null, creates a Vector(0, 0, 0)
        /// </param>
        public static Vector3 NewVector3(Vector3? inputVector, float givenX = 0f, float givenY = 0f, float givenZ = 0f)
        {
            Vector3 temp;

            if (inputVector == null)
            {
                temp = Vector3.zero;
            }
            else
            {
                temp = (Vector3)inputVector;
            }

            float X = givenX != 0 ? givenX : temp.x;
            float Y = givenY != 0 ? givenY : temp.y;
            float Z = givenZ != 0 ? givenZ : temp.z;

            return new Vector3(X, Y, Z);
        }
        
        /// <summary>
        /// Reduces the first vector by the second vector (first - sec), disregarding the z value
        /// </summary>
        public static Vector3 SubtractZ(Vector3 first, Vector3 sec)
        {
            return new Vector3(first.x - sec.x, first.y - sec.y);
        }

       

    }

    /// <summary>
    /// Collection of functions that can be used when dealing with random numbers
    /// </summary>

    public class RandomUtils
    {
        /// <summary>
        /// Returns either -1 or 1
        /// </summary>
        public static int Sign()
        {
            int h = Random.Range(-2, 2);
            if (h >0) return -1;
            return 1;
            
        }
    }


    /// <summary>
    /// Collections of functions that can be used when dealing with Cameras
    /// </summary>
    public class CameraUtils
    {
        private static Vector3 mousePos;
        public static Vector3 MousePosition { get
            {
                if (mousePos == null)
                    mousePos = GetMouseWorldPosition();
                return mousePos;
            }
        }
        /// <summary>
        /// Returns the Vector3 World Scaled position of the mouse
        /// </summary>
        public static Vector3 GetMouseWorldPosition()
        {
            Vector3 output = GetPosition(Input.mousePosition);
            mousePos = output;
            return output;
        }

        /// <summary>
        /// Returns the Vector3 World Scaled position of the mouse using the given camera
        /// </summary>
        public static Vector3 GetMouseWorldPosition(Camera givenCamera)
        {
            return GetPosition(Input.mousePosition, givenCamera);
        }

        /// <summary>
        /// Returns the Vector3 position of the given Screen/Pixel position using the Main Camera
        /// </summary>
        public static Vector3 GetPosition(Vector3 desiredPos)
        {
            return Camera.main.ScreenToWorldPoint(desiredPos);
        }

        /// <summary>
        /// Returns the Vector3 position of the given Screen/Pixel position using the given Camera
        /// </summary>

        public static Vector3 GetPosition(Vector3 desiredPos, Camera givenCamera)
        {
            
            return givenCamera.ScreenToWorldPoint(desiredPos);

        }


    }

    /// <summary>
    /// Collections of functions that can be used on time related activities.
    /// </summary>
    public class Time
    {
        /// <summary>
        /// Invokes the parameterless action in (duration) seconds. Uses the caller to start the Coroutine.
        /// </summary>
        public static void SetTimeout( Action action, float duration, MonoBehaviour caller)
        {
            caller.StartCoroutine(SetTimeout(action, duration));
        }

        /// <summary>
        /// Creates a new Monobehavior which it uses as the caller for the coroutine. Invokes the parameterless action in (duration) seconds. Note this method is not recommended and should be use only for DEBUGGING.
        /// </summary>
        public static void ForceSetTimeout(Action action, float duration)
        {
            new MonoBehaviour().StartCoroutine(SetTimeout(action, duration));
        }

        /// <summary>
        /// Invokes the parameterless action in (duration) seconds. Note: This function does not start the courotine. It has to be called, so the void Timeout is recommended.
        /// </summary>
        public static IEnumerator SetTimeout(Action action, float duration)
        {
            yield return new WaitForSeconds(duration);
            action?.Invoke();
        }


        /// <summary>
        /// Creates an interval that calls a parameterless action in every (duration) seconds. 
        /// </summary>
        /// <returns>Returns a coroutine that you can store to stop it.</returns>
        public static Coroutine SetInterval(Action action, float duration, MonoBehaviour caller)
        {
            return caller.StartCoroutine(SetInterval(action, duration));
        }
        
        /// <summary>
        /// Creates an interval of a parameterless action for every (duration) in seconds. Note: This needs a caller to start the return Coroutine value.
        /// </summary>
        public static IEnumerator SetInterval(Action action, float duration)
        {
            while(true)
            {
                yield return new WaitForSeconds(duration);
                action?.Invoke();
            }
        }

        /// <summary>
        /// Stops both Timeout and Intervals. Uses the caller to call the Stop Coroutine method.
        /// </summary>
        /// <param name="interval"></param>
        /// <param name="caller"></param>
        public static void StopAny(Coroutine interval, MonoBehaviour caller)
        {
            caller.StopCoroutine(interval);
        }



    }







}




