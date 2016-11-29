var smooth = 2.0;
var DoorOpenAngle = 90.0;
private var open : boolean;
private var enter : boolean;

private var defaultRot : Vector3;
private var openRot : Vector3;

function Start(){
    defaultRot = transform.eulerAngles;
    openRot = new Vector3 (defaultRot.x, defaultRot.y + DoorOpenAngle, defaultRot.z);
}

//Main function
function Update (){
    if(open){
        //Open door
        transform.eulerAngles = Vector3.Slerp(transform.eulerAngles, openRot, Time.deltaTime * smooth);
    }else{
        //Close door
        transform.eulerAngles = Vector3.Slerp(transform.eulerAngles, defaultRot, Time.deltaTime * smooth);
    }

    if(Input.GetKeyDown("e") && enter){
        open = !open;
    }
}




//Activate the Main function when player is near the door
function OnTriggerEnter (other : Collider){
    if (other.gameObject.tag == "Player") {
        enter = true;
    }
}

    //Deactivate the Main function when player is go away from door
    function OnTriggerExit (other : Collider){
        if (other.gameObject.tag == "Player") {
            enter = false;
        }
    }