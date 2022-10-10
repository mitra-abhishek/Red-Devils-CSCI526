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
    level_complete_df = pd.read_csv('data/Attempts Form.csv')
    #print(level_complete_df.head())
    level_complete_df['currentLevel'] = level_complete_df['currentLevel'].astype(str)

    level_time_df = pd.read_csv('data/CSCI526 Metrics.csv')
    #print(level_time_df.head())

    lvl_play_df = level_complete_df['currentLevel'].value_counts().reset_index()
    lvl_play_df.columns = ['level', 'count']
    lvl_play_df['Game Status'] = 'Started'

    #print(lvl_play_df.head())

    #print(level_complete_df[level_complete_df["isLevelCompleted"] == True])

    lvl_complete_df = level_complete_df[level_complete_df["isLevelCompleted"] == True]['currentLevel'].value_counts().reset_index()
    lvl_complete_df.columns = ['level', 'count']
    lvl_complete_df['Game Status'] = 'Completed'

    final_level_complete_df = pd.concat([lvl_play_df, lvl_complete_df], axis = 0)
    #print(final_level_complete_df.head())
    complete_plot = sns.barplot(x='level', y='count', hue='Game Status', data=final_level_complete_df)
    complete_plot.set_ylabel('No of times played')
    complete_plot.set_xlabel('Level')
    fig2 = complete_plot.get_figure()
    fig2.savefig("level_completion.png") 
    plt.clf()

    agg_time_df = level_time_df.groupby(['level']).agg(
                        min_completion_time = ('completionTime', 'min'),
                        mean_completion_time = ('completionTime', 'mean'),
                        max_completion_time = ('completionTime', 'max')
    )
    agg_time_df = agg_time_df.reset_index()
    agg_time_df['level'] = agg_time_df['level'].astype(str)
    time_plot = sns.barplot(data=agg_time_df, x="level", y="mean_completion_time")
    time_plot.set_ylabel('Avg Finish Time(in s)')
    time_plot.set_xlabel('Level')
    fig = time_plot.get_figure()
    fig.savefig("level_time.png")

    



    
    






