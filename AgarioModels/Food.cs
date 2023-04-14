using System.Numerics;

namespace AgarioModels
{
    /// <summary>
    /// Authors: Mason Sansom and Druv Rachakonda
    /// Date: 10-April-2023
    /// Course:    CS 3500, University of Utah, School of Computing
    /// Copyright: CS 3500, Mason Sansom and Druv Rachakonda - This work may not 
    ///            be copied for use in Academic Coursework.
    ///
    /// We, Mason Sansom and Druve Rachakonda, certify that we wrote this code from scratch and
    /// All references used in the completion of the assignments are cited 
    /// in the README file.
    ///
    /// File Contents
    /// Helps represent food objects
    /// </summary>
    public class Food : GameObject
    {
        public Food( float X, float Y, int ARGBColor, long ID, float Mass) : base(ID, new Vector2(X, Y), ARGBColor, Mass)
        {
            
        }

       
        
    }
}
