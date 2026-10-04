# RoReview: Advanced Solution for Sentiment Analysis, Aspect Extraction, and Sarcasm Detection

**RoReview** is a complete, end-to-end web application (Single Page Application) developed as part of my Bachelor's thesis. It serves as a research platform demonstrating how modern Deep Learning (Transformer) models can be adapted to understand informal Romanian text (e.g., e-commerce reviews, social media comments), with a special focus on **irony and sarcasm detection**. Furthermore, it integrates eXplainable AI (XAI) to provide transparent insights into the models' decision-making processes.

This project was also presented at the **AFCO** and **SCSS** student scientific sessions, where the feedback received contributed to the optimization of the application.

![Exemplu ABSA App](./Exemplu%20absa%20app.png)

## Core Machine Learning Pipeline

The system goes beyond simple binary classification, implementing a hybrid and intelligent processing workflow:

1.  **Sentiment Analysis (3-Class Classification):**
    The system classifies text as *Positive*, *Negative*, or *Neutral*. The base model, `BERTweetRO`, underwent sequential fine-tuning: first on the `REDv2` dataset (mapped from 7 emotions to 3 classes), and subsequently on `LaRoSeDa`. To address dataset imbalance, `LaRoSeDa` was supplemented with a manually extracted and annotated set of neutral reviews.
2.  **Sarcasm Detection (Domain Adaptation):**
    Sarcasm acts as a heuristic to reverse text polarity (e.g., an apparently positive text identified as sarcastic is re-evaluated as negative). The model was initially fine-tuned on journalistic and satirical corpora (`SaRoCo` and `SeLeRoSa`). Because applying automatically translated English datasets resulted in *Shortcut Learning* (the model learned translation artifacts rather than sarcasm), the final Domain Adaptation step for e-commerce reviews was achieved using a **synthetic dataset** generated and balanced via LLMs (Gemini).
3.  **Aspect-Based Sentiment Analysis (ABSA):**
    To overcome the lack of an annotated ABSA dataset for Romanian, a hybrid pipeline was developed. Reviews are segmented into sub-sentences and processed individually by the sentiment model. Aspects (subjects) are then extracted and syntactically validated using the dependency parsing trees provided by the `spaCy` library.
4.  **Explainable AI (XAI):**
    The inherent "black-box" nature of Transformer models was addressed by integrating the `LIME` (Local Interpretable Model-agnostic Explanations) algorithm. LIME assigns weights to the words that influenced the prediction in a specific direction. The results are visually represented in the UI (word opacity varies based on its predictive importance).

## Application Architecture (Client-Server)

*   **Frontend (UI):**
    Located in the `src` directory, the interface is a Single Page Application built with **React** and **Vite**. The codebase follows the **MVVM (Model-View-ViewModel)** architectural pattern using Custom Hooks, ensuring a clean separation of concerns between business logic and UI rendering.
*   **Backend (Processing Core):**
    Located in the `backend` directory, it is built in **Python** using the **FastAPI** framework, chosen for its asynchronous support necessary for Deep Learning inference. NLP models (`PyTorch`, `Hugging Face Transformers`) are loaded once upon server startup via a *Singleton* pattern, preventing excessive VRAM/RAM consumption and ensuring fast API response times.
*   **Database & Authentication:**
    Data persistence (analysis history, XAI results stored as JSON) is managed using **PostgreSQL** via the `SQLAlchemy` ORM, hosted on the **Supabase** platform. Authentication is secured using **JWT**, hashed passwords (`bcrypt`), admin approval gates for new accounts, and **Google OAuth** integration.
*   **Infrastructure:**
    During development and testing, the local backend is exposed securely via **Cloudflare Tunnel**, providing SSL/TLS encryption, DDoS protection, and WAF filtering.

## Technologies Used

*   **Machine Learning / NLP:** `PyTorch`, `Transformers` (Hugging Face), `spaCy`, `LIME`, `Scikit-Learn`
*   **Backend:** `Python`, `FastAPI`, `SQLAlchemy`, `Pydantic`
*   **Frontend:** `React`, `Vite`, `Axios`, `React Router`
*   **Database / Auth:** `Supabase` (PostgreSQL), `JSON Web Tokens (JWT)`, Google OAuth
*   **DevOps / Security:** `Cloudflare Tunnel`
