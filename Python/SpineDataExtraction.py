import json

# INPUT = "SpineData.txt"
INPUT = "TitsModSpineData.txt"
OUTPUT = "output.json"

EXTRACT_RULES = {
    "slaveClothesColorPartList": {
        "source": "slaveShowPartList",
        "match": lambda slot: "cloth" in slot
        # "match": lambda slot: "cloth" in slot and "maid" not in slot
    },
}

def load_json(path):
    with open(path, "r", encoding="utf-8") as f:
        return json.load(f)

def save_json(path, data):
    with open(path, "w", encoding="utf-8") as f:
        json.dump(data, f, ensure_ascii=False, indent=4)

def extract_slot(entry):
    if ":" not in entry:
        return None
    return entry.split(":", 1)[0]

def main():
    data = load_json(INPUT)

    result = {}

    for item in data.get("list", []):
        name = item.get("figure0Name", "").lower()
        # if name not in ["woman", "girl"]:
        # if name not in ["woman"]:
        if name not in ["girl"]:
            continue

        skeleton = item.get("skeletonDataAssetName")
        if not skeleton:
            continue

        if skeleton not in result:
            result[skeleton] = {key: set() for key in EXTRACT_RULES.keys()}

        for output_key, rule in EXTRACT_RULES.items():
            source_list = item.get(rule["source"], [])
            for entry in source_list:
                slot = extract_slot(entry)
                if slot and rule["match"](slot):
                    result[skeleton][output_key].add(slot)

    output_list = []
    for skeleton, part_dict in result.items():
        if all(len(values) == 0 for values in part_dict.values()):
            continue
        output_list.append({
            "skeletonDataAssetName": skeleton,
            **{key: sorted(list(values)) for key, values in part_dict.items()}
        })

    save_json(OUTPUT, {"list": output_list})
    print("Done! Output to", OUTPUT)

if __name__ == "__main__":
    main()