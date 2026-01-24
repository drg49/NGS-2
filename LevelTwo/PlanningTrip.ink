-> start

=== start ===
Ahh... Big Burger.. My favorite spot in town. The patty is perfectly seared, juicy and tender, melted cheese oozing over it, crisp lettuce, fresh tomato, and that toasted bun that smells like heaven. 
    + [Yeah it is pretty good.]
        -> planTrip("So, now that we're all here. I wanted to discuss a trip I had in mind. We are all free this weekend and I think it would be the perfect time. We haven't had an adventurous trip in a while. It's time we get out and do something as friends.")
    + [It's alright. I've had better.]
        -> hadBetter("Yeah, you're a picky eater though, so it's not like your opinion matters. You know damn well this is the best burger place in town. Better than any place you have taken us to! Just shut up and eat your burger.")

=== hadBetter(response) ===
{response}
    + [Lol. Right..]
        -> planTrip("So, now that we're all here. I wanted to discuss a trip I had in mind. We are all free this weekend and I think it would be the perfect time. We haven't had an adventurous trip in a while. It's time we get out and do something as friends.")

=== planTrip(response) ===
{response}
    + [What did you have in mind?]
        -> beachTrip("Well, the beach is only 4 hours away, and the hotels are reasonable this time of year since the weather isn't amazing. Most of the shops on the boardwalk will be closed, but we can still get a view of the ocean and maybe find a nice restaurant to eat some crab.")
    + [I don't know if I want to go on a trip. I'd rather relax at home.]
        -> ratherRelax("What's up with you lately? All you want to do is bail on us. Should I start calling you Mr. Flaky? It's always a mystery with you. Unreliable as hell..")

=== ratherRelax(response) ===
{response}
    + [Well okay.. Where did you want to go?]
        -> beachTrip("Well, the beach is only 4 hours away, and the hotels are reasonable this time of year since the weather isn't amazing. Most of the shops on the boardwalk will be closed, but we can still get a view of the ocean and maybe find a nice restaurant to eat some crab.")

=== beachTrip(response) ===
{response}
    + [4 hours is too far, and the beach is boring this time of year. Any other ideas?]
        -> campingTrip("Okay, well how about a fun camping trip in the woods up north? I have tons of equipment. I know a really nice spot where nobody will bother us. Camping is pointless when there's a bunch of other noisy campers nearby. I promise this place will be peaceful and worth the drive.")
    + [Summer is over, and I don't like the sand.]
        -> campingTrip("Okay, well how about a fun camping trip in the woods up north? I have tons of equipment. I know a really nice spot where nobody will bother us. Camping is pointless when there's a bunch of other noisy campers nearby. I promise this place will be peaceful and worth the drive.")

=== campingTrip(response) ===
{response}
    + [No. I just heard a story on the news where two women went missing near there.]
        -> chicken("Why are you such a chicken? I'm seriously getting tired of your paranoia... You gonna be a wimp your whole life or are you gonna live for once? Stop being so scared of everything, it's just a news story! C'mon you jerk, go camping with your friends...")
    + [Okay. That sounds a bit better than the beach.]
        -> great("Great, how about David and I meet you outside your apartment tomorrow morning. You can drive us up there in your brand new car. It's about a two hour drive, and there shouldn't be any traffic if we leave early in the morning.")

=== chicken(response) ===
{response}
    + [Alright geez.. Fine I'll go camping.]
        -> great("Great, how about David and I meet you outside your apartment tomorrow morning. You can drive us up there in your brand new car. It's about a two hour drive, and there shouldn't be any traffic if we leave early in the morning.")

=== great(response) ===
{response}
    + [Okay that's fine, I will book the reservation online.]
        -> forSure("For sure. Let's all pack our bags tonight. We're going to need tents, food, water, and some tools for starting a fire. We will not be happy campers if we don't get a fire started. I'll bring some skewers to cook hotdogs and marshmallows.")

=== forSure(response) ===
{response}
    + [Will do. I gotta use the restroom before I get out of here. I'll see you guys tomorrow morning.]
        -> final("Don't make a mess. See ya.")

=== final(response) ===
{response}

-> END
