# Contributing

There is one long-lived git branch, `master`.

`master` continuously deploys to the Azure AppService environment.
The AppService plan currently supports one deployment slot, so there is no staging environment in play.

## Starting new work

To begin new work, cut a new branch from `master` and name if after your work, maybe `feature1`. Push new changes to your feature branch up to VSTS often.

## Merging to `master`

When new work is complete, set up a Pull Request (PR) from `feature1` to `master`. Discussion about, and approval of changes happens in the PR interface in VSTS.

Once this new work is approved we complete the PR, merging the code.
From here, our CI pipeline will build the new changes on the `master` branch.  Next, our CD pipeline will deploy the new work to the Azure AppService environment.