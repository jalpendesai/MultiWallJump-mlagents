using UnityEngine;

using System.Collections;



public class PlatformGenerator : MonoBehaviour

{

    /* LICENSE

The MIT License (MIT)

 

Copyright (c) <2014> <TomásGonzález>

 

Permission is hereby granted, free of charge, to any person obtaining a copy

of this software and associated documentation files (the "Software"), to deal

in the Software without restriction, including without limitation the rights

to use, copy, modify, merge, publish, distribute, sublicense, and/or sell

copies of the Software, and to permit persons to whom the Software is

furnished to do so, subject to the following conditions:

 

The above copyright notice and this permission notice shall be included in

all copies or substantial portions of the Software.

 

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR

IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,

FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE

AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER

LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,

OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN

THE SOFTWARE.

     */

    public GameObject PlatformToSpawn; //reference to the Gameobject to spawn

    public float MaxPlatformWidth; //min - max values for the RNG

    public float MinPlatformWidth;//min - max values for the RNG

    public float MaxPlatformDistance;//min - max values for the RNG //Must be a positive number or 0

    public float MinPlatformDistance;//min - max values for the RNG //Must be a positive number or 0

    private float RandomWidth; //aux variable for transformation of the new platform

    private float RandomXDistance; //aux variable for transformation of the new platform

    private int platformCount; //aux variable for transformation of the new platform

    public int maxNumberOfPlatformFloors; //Self-explained

    private int currentNumberofPlatforms; //Self-explained



    // Use this for initialization

    void Start()

    {



    }



    // Update is called once per frame

    void Update()

    {

        //To generate the platforms we need to first check if we've reached our max, if not then invoke the generation method

        if (currentNumberofPlatforms < maxNumberOfPlatformFloors)

        {

            DoPlatform();

        }



    }



    void DoPlatform()

    {

        /*

         *This conditioning is to avoid a null reference to the previousPlatform Gameobject, as we won't have a previousPlatform when no platforms have been generated.

         *You can perfectly avoid this conditions if there's a reference to the previousPlatform (like setting some values at the Start method, or making a reference

         *to the parent gameObject, but this was easier to code :) )

         */

        if (currentNumberofPlatforms == 0)

        {

            //First we need to generate the random numbers that we will use in our current platform

            RandomWidth = Random.Range(MinPlatformWidth, MaxPlatformWidth);

            RandomXDistance = Random.Range(MinPlatformDistance, MaxPlatformDistance);

            //Then we create the platform...

            GameObject platformGenerated = Instantiate(PlatformToSpawn, transform.position, transform.rotation) as GameObject;

            //and then we adjust its scale and position acording to the numbers that we got from the RNG's

            platformGenerated.transform.localScale += new Vector3(RandomWidth, 0, 0);

            platformGenerated.transform.Translate(RandomXDistance, 0, 0);

            //Finally we name our platform, and add a count to the variables that verifies the current number of platforms in the Update method.

            platformCount++;

            platformGenerated.name = "Platform N°" + platformCount;

            currentNumberofPlatforms++;

            /*

            The first platform is the easier one :P

            This is because there's no other platform in its X axis that it may interfere with(be on the same place as other platform)

            To fix this little issue we do the following:

             */

        }

        else

        {

            //First we need a reference to the previous platform, so we search it by name

            GameObject previousPlatform = GameObject.Find("Platform N°" + platformCount);

            //We also need to generate the random numbers (This is the procedural part of the game)

            RandomWidth = Random.Range(MinPlatformWidth, MaxPlatformWidth);

            RandomXDistance = Random.Range(MinPlatformDistance, MaxPlatformDistance);

            //We create a new platform..

            GameObject platformGenerated = Instantiate(PlatformToSpawn, transform.position, transform.rotation) as GameObject;

            //and  adjust its scale to the RNG generated width

            platformGenerated.transform.localScale += new Vector3(RandomWidth, 0, 0);

            /*

             * Now, this is the tricky part, so I will explain it a little more. The challenge of this problem is that we need to figure a way of avoiding the platforms to 

             * interfere with one another. A way of solving this issue is to make an automatic translation that will, before any onther movement of the platform,

             * set the position of the new platform EXACTLY at the "Edge" of the previous platform, this will make any proceding modificaton to the X axis of the new platform

             * to not interfere with the previously generated object. NOTE : as you may have already figured out, the procedurally generated movement should always be

             * a positive value of X, or it may generate some juxtaposition of elements. This may be fixed by adding a boolean test to determine wether the RNG generated number

             * represents a positive or negative translation, and then invert the initial translation that puts our object at the "Edge" of the previous object. I decided

             * not to do this as it complicates an already complicated script (at least for a C# Noob), but it should be fairly easy to do if you feel that you need this modification

             */

            //We first put move the new platform from its gameobject position to be right in the same place as the previous platform

            platformGenerated.transform.localPosition = previousPlatform.transform.localPosition;

            //Then we add a translation tha moves our object by an amount equal to half the X size of the previous object, and half of the X size of the new Platform

            platformGenerated.transform.Translate(previousPlatform.GetComponent<Renderer>().bounds.extents.x + platformGenerated.GetComponent<Renderer>().bounds.extents.x, 0, 0);

            //And now, we may add any positive X axis movement and it will never interfere with other platforms (go ahead, and try to see what happens if the min and maxDistance are set to 0)

            platformGenerated.transform.Translate(RandomXDistance, 0, 0);

            //Finally we give a name to the platform, and add a count to the variables that verifies the current number of platforms in the Update method

            platformCount++;

            platformGenerated.name = "Platform N°" + platformCount;

            currentNumberofPlatforms++;

        }



    }

}