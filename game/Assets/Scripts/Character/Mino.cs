using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mino : MonoBehaviour
{
    [SerializeField] List<Sprite> sprites;
    SpriteAnimator spriteAnimator;

    // Start is called before the first frame update
    void Start()
    {
        spriteAnimator = new SpriteAnimator(sprites, GetComponent<SpriteRenderer>());
        Timings.DelayAction(1f);
    }

    // Update is called once per frame
    void Update()
    {
        spriteAnimator.HandleUpdate();
    }

    private void FixedUpdate()
    {
        Vector3 pos = transform.position;
        pos.x += 3f * Time.deltaTime;
        transform.position = pos;
    }
}
