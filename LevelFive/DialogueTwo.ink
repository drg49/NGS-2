-> start

=== start ===
[Marcus]: This fire sure is nice. Honestly, this might be my favorite campground. Mostly because nobody ever comes out this far.
    + [Yeah... it's really quiet.]
        -> resOne("[Marcus]: Heh, yeah. Just us, the trees, and the fire. Kinda peaceful, right?")
    + [It's kinda weird nobody's here though.]
        -> resOne("[Marcus]: Probably the roads. Most cars wouldn't survive that drive in. Fine by me, less people.")

=== resOne(response) ===
{response}
    + [Yeah, it's relaxing.]
        -> resHike("[Marcus]: Oh - hey, you guys still up for that hike tomorrow? There's a lake at the end of the trail. Only about an hour walk.")
    + [So what's the plan for tomorrow?]
        -> resHike("[Marcus]: I was thinking we hit that trail nearby. There's a lake at the end of it. Only about an hour walk.")

=== resHike(response) ===
{response}
    + [Let's do it.]
        -> resThree("[David]: We should leave in the morning. maybe around 11.")
    + [Sure, but we should probably go to sleep early if we want to do that.]
        -> resThree("[David]: Agreed. That way we can head out in the morning without feeling like crap.")

=== resThree(response) ===
{response}
    + [Continue]
        -> lastRes("[David]: Let's get out the hot dogs and some wine, it's dinner time!")

=== lastRes(response) ===
{response}       

-> END
