import torch
import torch.nn as nn
import numpy as np
from transformers import AutoTokenizer, AutoModel
import pickle

# Load the label encoders
with open("model/label_encoders.pkl", "rb") as f:
    label_encoders = pickle.load(f)


# Define the model class
class SimpleNN(nn.Module):
    def __init__(self, input_dim):
        super(SimpleNN, self).__init__()
        self.fc1 = nn.Linear(input_dim, 128)
        self.fc2 = nn.Linear(128, 64)
        self.fc3 = nn.Linear(64, 1)
        self.sigmoid = nn.Sigmoid()

    def forward(self, x):
        x = torch.relu(self.fc1(x))
        x = torch.relu(self.fc2(x))
        x = self.fc3(x)
        return self.sigmoid(x)


# Load your trained model
model = SimpleNN(input_dim=389)
model.load_state_dict(torch.load("model/model.pth", map_location=torch.device("cpu")))
model.eval()

# Load your tokenizer and embedding model for text
tokenizer = AutoTokenizer.from_pretrained("BAAI/bge-small-en")
text_model = AutoModel.from_pretrained("BAAI/bge-small-en")


# Function to get text embeddings
def get_text_embeddings(text, tokenizer, model):
    encoded_input = tokenizer(
        text, return_tensors="pt", padding=True, truncation=True, max_length=50
    )

    with torch.no_grad():
        model_output = model(**encoded_input)

    sentence_embeddings = model_output[0][:, 0]
    return sentence_embeddings.numpy()


# Function to predict pairing
def predict_pairing(food_description, wine_features):
    text_embedding = get_text_embeddings(food_description, tokenizer, text_model)
    text_embedding = np.vstack(text_embedding)

    wine_features = wine_features.reshape(1, -1)
    combined_features = np.hstack([text_embedding, wine_features])
    combined_features = combined_features.astype(np.float32)

    combined_features_tensor = torch.tensor(combined_features, dtype=torch.float32)

    with torch.no_grad():
        prediction = model(combined_features_tensor)

    predicted_probability = prediction.item()
    is_pairing = predicted_probability > 0.5

    return is_pairing


# Export the label encoders for use in the main Flask app file
def get_label_encoders():
    return label_encoders
