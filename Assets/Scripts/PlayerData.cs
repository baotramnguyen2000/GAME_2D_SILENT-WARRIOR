using System.Collections.Generic;
[System.Serializable]
public class PlayerData
{
    public float[] position;
    public int currentHealth;
    public int scene;
    public List<string> listitemTag;
    public float timeValue;
    public PlayerData (PlayerController player)
    {
        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;

        currentHealth = player.currentHealth;
        scene = player.sceneCurrent;

        listitemTag = player.listitemTag;
        timeValue = player.timeValue;
    }
}
