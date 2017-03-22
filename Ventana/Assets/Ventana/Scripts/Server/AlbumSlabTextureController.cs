using UnityEngine;
using System.Collections;
using SocketIO;

public class AlbumSlabTextureController : MonoBehaviour {
    public string newURL;
    public Texture oldTexture;
    public SocketIOComponent socket;

    private void Awake() {
        socket = VentanaRequestFactory.Instance.socket;
    }

    void Start() {
        newURL = "http://is5.mzstatic.com/image/thumb/Music3/v4/47/97/af/4797af7e-24c9-7428-ac64-5b5f35eba51e/source/100000x100000-999.jpg";
        WWW www = new WWW(newURL);
        StartCoroutine(ChangeAlbumTexture(www));
        socket.On("push", HandlePush);
        StartCoroutine("BeepBoop");
    }

    private IEnumerator BeepBoop() {
        // wait 1 seconds and continue
        yield return new WaitForSeconds(1);

        socket.Emit("beep");

        // wait 3 seconds and continue
        yield return new WaitForSeconds(3);

        socket.Emit("beep");

        // wait 2 seconds and continue
        yield return new WaitForSeconds(2);

        socket.Emit("beep");

        // wait ONE FRAME and continue
        yield return null;

        socket.Emit("beep");
        socket.Emit("beep");
    }

    public void HandlePush(SocketIOEvent e) {
        Debug.Log("[SocketIO] Boop received: " + e.name + " " + e.data);
    }

    // Update is called once per frame
    void Update() {


    }

    IEnumerator ChangeAlbumTexture(WWW www) {
        yield return www;

        
        

        if ( www.error == null ) {
            var materials = gameObject.GetComponent<Renderer>().materials;
            materials[1].mainTexture = www.texture;

            //www.LoadImageIntoTexture(renderer.material.mainTexture);

            //www.LoadImageIntoTexture(gameObject.GetComponent<Renderer>().materials[1].tex)
            //gameObject.GetComponent<Renderer>().materials[1].SetTexture("album", www.texture);
            gameObject.GetComponent<Renderer>().materials = materials;
            // Debug.Log(www.texture);


        } else {
            //Debug.Log("Not changing texture");
        }
    }

    void OnURLSent(VentanaInteractable venta) {
        //album art contains the URL
        SonosInfo info = venta as SonosInfo;
        //Debug.Log(info.album_art);
        if ( newURL != info.album_art ) {
            Debug.Log("Im getting changed");
            WWW www = new WWW(info.album_art);
            newURL = info.album_art;
            StartCoroutine(ChangeAlbumTexture(www));

        }
    }
}
