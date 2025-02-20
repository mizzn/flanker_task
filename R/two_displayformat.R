library(effectsize)
library(emmeans)

source("functions.R")

# formからのデータをよみだす
form_data = read.csv("csv/JNNS2024_flanker_answer.csv", header = TRUE, sep = ",")
# View(form_data)

# ID(ファイル名)を取得
ids = form_data[, 7]
# print(id[1])
# print(id[2])

# 分析用dfを作っておく
df = data.frame(
  displayFormat = character(), # 表示形式
  incongruency = character(), # 一致不一致
  RT_ave = numeric() # RT
)
View(df)

file_names = c("controll", "2D", "barrier", "Far")
# display_formats = c("VR Object", "2D Object", "VR Object with barrier", "VR Object with distance)
# id = "ZAQJ"

for (id in ids) {
  for (file_name in file_names){
    display_format = get_display_format(file_name)
    # パスつくる
    path = paste0("csv/", id, "/")
    # print(path)
    
    # csv読み出して確認
    response_data = read.csv(paste0(path, id, file_name,".csv"), header = TRUE, sep = ",")
    paste0(path, id, file_name,".csv")
    # View(response_data)
    
    # 要因ごとに処理
    congruent_data = chose_by_incongruency(response_data, "congruent")
    # View(congruent_data)
    filtered_congruent = remove_unnecessary_data(congruent_data)
    # View(filtered_congruent)
    congruent_ave = mean(filtered_congruent$RT, na.rm = TRUE)
    df = add_row(df, display_format, "congruent", congruent_ave)
    # View(df)
    
    incongruent_data = chose_by_incongruency(response_data, "incongruent")
    filtered_incongruent = remove_unnecessary_data(incongruent_data)
    # View(filtered_data)
    incongruent_ave = mean(filtered_incongruent$RT, na.rm = TRUE)
    df = add_row(df, display_format, "incongruent", incongruent_ave)
    # View(df)
  }
}

View(df)


# VRと2Dで比較
df1 <- subset(df, displayformat == "2D Object" | displayformat == "VR Object")
View(df1)
AOV1 <- aov(RT_ave ~ incongruency + displayformat + incongruency * displayformat, data = df1)
summary(AOV1)
eta_squared(AOV1)
emmip(AOV1, incongruency ~ displayformat, CIs = TRUE)

# 2Dとbarrierで比較
df2 <- subset(df, displayformat == "2D Object" | displayformat == "VR Object with barrier")
View(df2)
AOV2 <- aov(RT_ave ~ incongruency + displayformat + incongruency * displayformat, data = df2)
summary(AOV2)
eta_squared(AOV2)
emmip(AOV2, incongruency ~ displayformat, CIs = TRUE)

# 2Dとfarで比較
df3 <- subset(df, displayformat == "2D Object" | displayformat == "VR Object with distance")
View(df3)
AOV3 <- aov(RT_ave ~ incongruency + displayformat + incongruency * displayformat, data = df3)
summary(AOV3)
eta_squared(AOV3)
emmip(AOV3, incongruency ~ displayformat, CIs = TRUE)

# VRとbarrierで比較
df4 <- subset(df, displayformat == "VR Object" | displayformat == "VR Object with barrier")
View(df4)
AOV4 <- aov(RT_ave ~ incongruency + displayformat + incongruency * displayformat, data = df4)
summary(AOV4)
eta_squared(AOV4)
emmip(AOV4, incongruency ~ displayformat, CIs = TRUE)

# VRとFarで比較
df5 <- subset(df, displayformat == "VR Object" | displayformat == "VR Object with distance")
View(df5)
AOV5 <- aov(RT_ave ~ incongruency + displayformat + incongruency * displayformat, data = df5)
summary(AOV5)
eta_squared(AOV5)
emmip(AOV5, incongruency ~ displayformat, CIs = TRUE)

# barrierとFarで比較
df6 <- subset(df, displayformat == "VR Object with barrier" | displayformat == "VR Object with distance")
View(df6)
AOV6 <- aov(RT_ave ~ incongruency + displayformat + incongruency * displayformat, data = df6)
summary(AOV6)
eta_squared(AOV6)
emmip(AOV6, incongruency ~ displayformat, CIs = TRUE)
