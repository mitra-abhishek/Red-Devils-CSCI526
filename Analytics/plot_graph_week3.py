from time import time
import pandas as pd
import numpy as np
from tqdm import tqdm

import matplotlib.pyplot as plt
import seaborn as sns
from prettytable import PrettyTable
from IPython import display


def change_width(ax, new_value) :
    for patch in ax.patches :
        current_width = patch.get_width()
        diff = current_width - new_value

        # we change the bar width
        patch.set_width(new_value)

        # we recenter the bar
        patch.set_x(patch.get_x() + diff * .5)


if __name__ == '__main__':
    import warnings
    warnings.filterwarnings("ignore")
    plt.rcParams["figure.figsize"] = (7,5)
    sns.set(style="white", font_scale=1.1)
    sns.color_palette("bright")
    level_char_df = pd.read_csv('data/characters_shot.csv')
    level_char_df = level_char_df.rename({'currentLevel': 'level'}, axis=1)
    level_char_df = level_char_df[level_char_df['level'] != 0]
    agg_level_char_total_df = level_char_df.groupby(['level']).agg(
                        char_shot = ('totalCharactersShot', 'mean')
    )
    agg_level_char_total_df['Characters Shot'] = 'Total'
    agg_level_char_total_df.reset_index(inplace=True)
    agg_level_char_correct_df = level_char_df.groupby(['level']).agg(
                        char_shot = ('correctCharacterShot', 'mean')
    )
    agg_level_char_correct_df['Characters Shot'] = 'Correct'
    agg_level_char_correct_df.reset_index(inplace=True)


    #print(agg_level_char_total_df.head())
    #print(agg_level_char_correct_df.head())

    final_char_shot_df = pd.concat([agg_level_char_total_df, agg_level_char_correct_df], axis = 0)

    char_shot_plot = sns.barplot(x='level', y='char_shot', hue='Characters Shot', data=final_char_shot_df, palette=sns.color_palette("bright"))
    char_shot_plot.set_ylabel('Avg Characters Shot per game')
    char_shot_plot.set_xlabel('Level')
    #change_width(char_shot_plot, 0.7)

    fig2 = char_shot_plot.get_figure()
    fig2.savefig("level_char_shot.png")
    plt.clf()

    level_powerup_df = pd.read_csv('data/powerups_shot.csv')
    level_powerup_df = level_powerup_df.rename({'currentLevel': 'level'}, axis=1)
    level_powerup_df = level_powerup_df[level_powerup_df['level'] != 0]
    agg_level_powerup_df = level_powerup_df.groupby(['level']).agg(
                        total_powerups = ('totalPowerUps', 'mean'),
                        collected_powerups = ('collectedPowerUps', 'mean')              
    )
    agg_level_powerup_df.reset_index(inplace=True)
    agg_level_powerup_df['Mean % of Powerups used per game'] = (agg_level_powerup_df['collected_powerups'] / agg_level_powerup_df['total_powerups']) * 100
    #print(agg_level_powerup_df.head())

    agg_level_powerup_df['level'] = agg_level_powerup_df['level'].astype(str)
    powerup_plot = sns.barplot(data=agg_level_powerup_df, x="level", y='Mean % of Powerups used per game', palette=sns.color_palette("bright"))
    powerup_plot.set_ylabel('Avg % of Powerups used per game')
    powerup_plot.set_xlabel('Level')
    #powerup_plot.bar_label(powerup_plot.containers[0])
    change_width(powerup_plot, 0.7)
    fig = powerup_plot.get_figure()
    fig.savefig("level_powerup.png")










