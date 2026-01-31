using UnityEngine;

public class CollideerCheckLoseWin : MonoBehaviour {
    public bool isWinObj = true;
    private void OnTriggerStay2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            if (isWinObj) {

                GameplayManager.Ins.Win();
            }
            else {

                GameplayManager.Ins.Lose();
            }
        }
    }
}
