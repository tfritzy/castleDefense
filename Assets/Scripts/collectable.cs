using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public abstract class collectable : MonoBehaviour {

	public GameObject sparkle;
	protected int value;
	protected float startTime;
	protected bool isGoingToLabel;
	public GameObject targetLabel;
    public string collectionSoundEffectName;

    public abstract void GiveValue();
    public abstract void SetTargetLabel();
    float instTime;

    void Start(){
		startTime = Time.time;
		this.isGoingToLabel = false;
        this.instTime = Time.time;
        this.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-50f, 50f), 200f));
        this.collectionSoundEffectName = "acqBronze";

	}

    public virtual void MakeCollectionSoundEffect()
    {

    }

	void Update(){
        if (Time.time > instTime + 15f)
        {
            Destroy(this.gameObject);
        }

		if (!isGoingToLabel) {
			if (Input.touchCount > 0) {
				if ((Input.GetTouch (0).position - (Vector2)this.transform.position).magnitude < 2.0f) {
					Reward ();
				}
			}
			if (((Vector2)Camera.main.ScreenToWorldPoint (Input.mousePosition) - (Vector2)this.transform.position).magnitude < 2.0f) {
				Reward ();
			}
			if (Time.time > startTime + 6f) {
				Reward ();
			}
		} else {
            
			Vector2 diff = (Vector2)targetLabel.transform.position - (Vector2)this.transform.position;
			if ((diff).magnitude > 1f) {
				this.GetComponent<Rigidbody2D> ().velocity = new Vector2(diff.x * 3f, diff.y * 3f);
			} else {
                GiveValue();
				Destroy (this.gameObject);
			}

            if (targetLabel == null)
            {
                Destroy(this.gameObject);
            }
		}
	}

	public void SetValue(int value){
		this.value = value;
	}

   

	void Reward(){
        // Set Target Label needs to happen first because it gives inheriting classes a chance to set the sound effect they want to play.
        SetTargetLabel();
        Debug.Log(this.collectionSoundEffectName);
        AudioManager.manager.Play(this.collectionSoundEffectName);
        Destroy(this.GetComponent<BoxCollider2D>());
        GameObject sparkleInst = Instantiate (sparkle);
		sparkleInst.transform.position = this.transform.position;
		Destroy (sparkleInst, sparkleInst.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0).length / sparkleInst.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0).speed);
        this.isGoingToLabel = true;
    }
}
