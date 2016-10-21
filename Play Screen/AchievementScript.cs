using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class AchievementScript : GPSAdapter {

    public List<Achievement> achievements { get; set; }

	// Use this for initialization
	void Start () {
        achievements = new List<Achievement>();
        Achievement a1 = new Achievement("aTotal", "Current level");
        a1.nextLevel = new int[] { 50, 100, 200, 500, 1000, 2000, 3000, 5000 }.Concat<int>(Enumerable.Range(1, int.MaxValue - 1).Select(x => x * 10000));
        a1.update = () => { if (PlayerPrefs.GetFloat("gTotalDistance") > a1.nextLevel.ElementAt(a1.level)) { a1.levelUp(); } };
        a1.nextGoal = () => { return String.Format("Goal: Walk {0}m", a1.nextLevel.ElementAt(a1.level)); };
		a1.currentGoal = () => { return a1.nextLevel.ElementAt(a1.level); };

        achievements.Add(a1);
        Achievement a2 = new Achievement("aSession", "Current level");
		a2.nextLevel = new int[] { 250, 500, 1000, 1500 }.Concat<int>(Enumerable.Repeat(2500, int.MaxValue));
		a2.update = () => { if (a2.available == true && PlayerPrefs.GetFloat("gSessionDistance") > a2.nextLevel.ElementAt(a2.level)) { a2.available = false; a2.levelUp(); } };
        a2.nextGoal = () => { return String.Format("Goal: Walk {0}m in single session", a2.nextLevel.ElementAt(a2.level)); };
		a2.currentGoal = () => { return a2.nextLevel.ElementAt(a2.level); }; //rubbish value - do not use
        achievements.Add(a2);
        Achievement a3 = new Achievement("aSpeed", "Timed Session (30 minutes)");
		a3.nextLevel = new int[] { 250, 500, 1000, 1500 }.Concat<int>(Enumerable.Repeat(2500, int.MaxValue));
		a3.update = () => { if (a3.available == true && Time.time - PlayerPrefs.GetFloat("gSessionStartTime") < 30 * 60 && PlayerPrefs.GetFloat("gSessionDistance") > a3.nextLevel.ElementAt(a3.level)) { a3.available = false; a3.levelUp(); }; };
        a3.nextGoal = () => { return String.Format("Walk {0}m in a single 30 minutes session", a3.nextLevel.ElementAt(a3.level)); };
		a3.currentGoal = () => { return a3.nextLevel.ElementAt(a3.level); };
        achievements.Add(a3);
        foreach (Achievement a in achievements)
        {
            a.level = PlayerPrefs.GetInt(a.key, 0);
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void onTrack(float distance)
    {
        foreach (Achievement a in achievements)
        {
            a.update();
        }
    }

	public void newSession()
	{
		foreach (Achievement a in achievements)
		{
			a.newSession();
		}
	}
		

    public class Achievement
    {
        public int level { get; set; }
		public string key { get; set; }
		public string name { get; set; }

        public IEnumerable<int> nextLevel = new int[]{ };

        public Action levelUp; //initialised in constructor
        public Action update = () => { };
		public Action newSession;
		public bool available = true;

        public Func<string> nextGoal = () => { return ""; } ;
		public Func<int> currentGoal = () => {
			return 0;
		};

        public Achievement(string key, string name)
        {
            this.key = key;
            this.name = name;
			this.level = PlayerPrefs.GetInt(key, 0);
            this.levelUp = () => PlayerPrefs.SetInt(key, ++level);
			this.newSession = () => { available = true; };
        }

    }

}
