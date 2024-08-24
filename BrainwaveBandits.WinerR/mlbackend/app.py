from flask import Flask, request, jsonify
import pandas as pd
import numpy as np
from audiowine import audio_to_wines
from predictor import predict_pairing, get_label_encoders
import os


# Flask app
app = Flask(__name__)

# Get the label encoders
label_encoders = get_label_encoders()


@app.route("/predictwine", methods=["POST"])
def predict():
    data = request.json
    food_description = data["mainIngredient"]["name"]
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
    try:
        # Check if content-type is correct
        if request.content_type != 'audio/wav':
            return jsonify({"error": "Unsupported Media Type"}), 415

        # Ensure the directory exists or create a temp folder in the current directory
        audio_dir = os.path.join(os.getcwd(), "uploaded_audio")
        os.makedirs(audio_dir, exist_ok=True)  # Create the directory if it doesn't exist

        # Save the raw audio data to a file
        audio_path = os.path.join(audio_dir, "uploaded_audio.wav")
        with open(audio_path, "wb") as f:
            f.write(request.data)

        # Process the audio file and get predictions
        matched_wines, non_matched_wines = audio_to_wines(audio_path)
        
        print(jsonify({
            "MatchedWines": matched_wines,
            "NonMatchedWines": list(non_matched_wines)
        }))

        return jsonify({
            "MatchedWines": matched_wines,
            "NonMatchedWines": list(non_matched_wines)
        })

    except Exception as e:
        # Log the error
        app.logger.error(f"Error processing the audio file: {str(e)}")
        return jsonify({"error": "Internal Server Error", "message": str(e)}), 500






if __name__ == "__main__":
    app.run(host="0.0.0.0", port=5002)
