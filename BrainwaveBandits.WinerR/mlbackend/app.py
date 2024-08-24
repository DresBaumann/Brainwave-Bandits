from flask import Flask, request, jsonify
import pandas as pd
import numpy as np
from audiowine import audio_to_wines
from predictor import predict_pairing, get_label_encoders
import logging

logging.basicConfig(
    level=logging.DEBUG,  # Set the minimum log level (DEBUG, INFO, WARNING, ERROR, CRITICAL)
    format="%(asctime)s - %(levelname)s - %(message)s",
)


# Flask app
app = Flask(__name__)

# Get the label encoders
label_encoders = get_label_encoders()


@app.route("/predictwine", methods=["POST"])
def predict():
    data = request.json

    logging.debug(data)

    food_description = data["MainIngredient"]["Name"]
    wine_features_df = pd.read_csv("data/wine_small.csv")[
        ["Type", "Elaborate", "Body", "Acidity", "ABV"]
    ]

    categorical_cols = ["Type", "Elaborate", "Body", "Acidity"]
    for col in categorical_cols:
        wine_features_df[col] = label_encoders[col].transform(wine_features_df[col])

    wine_features_list = wine_features_df[
        ["Type", "Elaborate", "Body", "Acidity", "ABV"]
    ].values
    id_list = pd.read_csv("data/wine_small.csv")[["WineID"]].values.tolist()

    results = []
    for i, wine_features in enumerate(wine_features_list):
        is_pairing = predict_pairing(food_description, wine_features)
        results.append(
            {
                "WineId": id_list[i],
                "IsPairing": is_pairing,
            }
        )

    return jsonify(results)


@app.route("/audiowines", methods=["POST"])
def predict_audio():
    data = request.json
    audio_path = data["audio_path"]
    matched_wines, non_matched_wines = audio_to_wines(audio_path)
    return jsonify(
        {
            "matched_wines": matched_wines,
            "non_matched_wines": list(non_matched_wines),
        }
    )


if __name__ == "__main__":
    app.run(host="0.0.0.0", port=5002)
