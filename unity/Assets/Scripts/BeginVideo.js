

  //var movTexture : MovieTexture;
    function OnMouseDown() { 
	    
	
	    /*renderer.material.mainTexture.Play(); 
		audio.clip = renderer.material.mainTexture.audioClip;
		audio.Play();*/
        //renderer.material.mainTexture = movTexture;
        //movTexture.Play();
    }
	
/*	   var url = "http://www.unity3d.com/webplayers/Movie/sample.ogg";
    function Start () {
        // Start download
        var www = new WWW(url);
        // Make sure the movie is ready to start before we start playing
        var movieTexture = www.movie;
        while (!movieTexture.isReadyToPlay)
            yield;
        // Initialize gui texture to be 1:1 resolution centered on screen
		renderer.material.mainTexture = movieTexture;
        ////guiTexture.texture = movieTexture;
        ////transform.localScale = Vector3 (0,0,0);
        ////transform.position = Vector3 (0.5,0.5,0);
        ////guiTexture.pixelInset.xMin = -movieTexture.width / 2;
        /////guiTexture.pixelInset.xMax = movieTexture.width / 2;
        ////guiTexture.pixelInset.yMin = -movieTexture.height / 2;
        ////guiTexture.pixelInset.yMax = movieTexture.height / 2;
        // Assign clip to audio source
        // Sync playback with audio
        audio.clip = movieTexture.audioClip;
        // Play both movie & sound
        movieTexture.Play();
        audio.Play();
    }
    // Make sure we have gui texture and audio source
    //@script RequireComponent (GUITexture)
    //@script RequireComponent (AudioSource)
*/	