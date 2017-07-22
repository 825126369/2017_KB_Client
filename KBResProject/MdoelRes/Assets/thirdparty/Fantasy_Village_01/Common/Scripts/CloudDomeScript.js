var scrollSpeed = 1.0;
var scrollDirection = 0.0;

function Update () {
    var offset = Time.time * scrollSpeed;
    GetComponent.<Renderer>().material.SetTextureOffset ("_MainTex", Vector2(offset,0));
	transform.eulerAngles.y = scrollDirection;
}