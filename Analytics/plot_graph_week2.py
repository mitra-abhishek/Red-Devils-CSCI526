from time import time
import pandas as pd
import numpy as np
from tqdm import tqdm

import matplotlib.pyplot as plt
import seaborn as sns
from prettytable import PrettyTable
from IPython import display


import warnings
warnings.filterwarnings("ignore")
plt.rcParams["figure.figsize"] = (7,5)
sns.set(style="white", font_scale=1.1)


if __name__ == '__main__':
    level_health_df = pd.read_csv('data/health_form.csv')
    level_health_df = level_health_df.rename({'currentLevel': 'level'}, axis=1)
    agg_health_df = level_health_df.groupby(['level']).agg(
                        min_health = ('remainingHealth', 'min'),
                        mean_health = ('remainingHealth', 'mean'),
                        max_health = ('remainingHealth', 'max')
    )
    agg_health_df = agg_health_df.reset_index()
    agg_health_df['level'] = agg_health_df['level'].astype(str)
    time_plot = sns.barplot(data=agg_health_df, x="level", y="mean_health")
    time_plot.set_ylabel('Avg Health Left')
    time_plot.set_xlabel('Level')
    fig = time_plot.get_figure()
    fig.savefig("level_health_left.png")
    plt.clf()

    level_death_df = pd.read_csv('data/death_form.csv')
    #level_death_df['currentLevel'] = level_death_df['currentLevel'].astype(str)
    level_death_df = level_death_df.rename({'currentLevel': 'level'}, axis=1)
    level_death_df = level_death_df.rename({'deathReason': 'Game Over Reason'}, axis=1)


    complete_plot = sns.countplot(x='level', hue='Game Over Reason', data=level_death_df)
    # make the y ticks integers, not floats
    yint = []
    locs, labels = plt.yticks()
    for each in locs:
        yint.append(int(each))
    plt.yticks(yint)
    complete_plot.set_ylabel('No of Game Overs')
    complete_plot.set_xlabel('Level')
    fig2 = complete_plot.get_figure()
    fig2.savefig("level_finish_reason.png") 



