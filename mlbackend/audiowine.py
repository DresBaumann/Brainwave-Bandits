from difflib import get_close_matches
import pandas as pd
from openai import OpenAI

# load env variable
from dotenv import load_dotenv

load_dotenv()


client = OpenAI()


def transcribe_audio(wav_path):
    audio_file = open(wav_path, "rb")
    transcription = client.audio.transcriptions.create(
        model="whisper-1", file=audio_file, language="en"
    )
    print(transcription.text)
    return transcription.text


import json


def extact_wine_names(transcribed_text):
    prompt = f"""Extract all wine names from the following text: {transcribed_text}. Return them as a comma-separated list. Return in JSON format as below:
    
    {{\"wine_names\": [\"wine_name1\", \"wine_name2\", ...]}}"""

    response = client.chat.completions.create(
        model="gpt-4o",
        messages=[
            # {"role": "system", "content": "You are a helpful assistant that responds in JSON format."},
            {"role": "user", "content": prompt}
        ],
        temperature=0.5,
        response_format={"type": "json_object"},
        max_tokens=150,
    )
    json_response = response.choices[0].message.content
    json_response = json.loads(json_response)
    wine_names = json_response["wine_names"]
    return wine_names


def match_wine_names(wine_names, wine_df):
    matched_wines = []
    non_matched_wines = []
    for wine in wine_names:
        matches = get_close_matches(wine, wine_df["WineName"], n=1, cutoff=0.7)
        if matches:
            matched_wine_name = matches[0]
            wine_id = str(
                wine_df.loc[wine_df["WineName"] == matched_wine_name, "WineID"].values[
                    0
                ]
            )
            matched_wines.append({"WineName": matched_wine_name, "WineID": wine_id})
        else:
            non_matched_wines.append({"WineName": wine})

    return matched_wines, non_matched_wines


def audio_to_wines(wav_path):
    wine_df = pd.read_csv("data/wine_small.csv")

    transcribed_text = transcribe_audio(wav_path)
    wine_names = extact_wine_names(transcribed_text)
    matched_wines, non_matched_wines = match_wine_names(wine_names, wine_df)
    return matched_wines, non_matched_wines
