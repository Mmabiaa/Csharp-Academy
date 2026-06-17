using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CsharpAcademy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSeedData_Fresh : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "LessonVideos",
                keyColumn: "Id",
                keyValue: 1,
                column: "VideoUrl",
                value: "https://youtu.be/ravLFzIguCM");

            migrationBuilder.UpdateData(
                table: "LessonVideos",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Title", "VideoUrl" },
                values: new object[] { "Every C# feature in 10 minutes", "https://youtu.be/J0FhV3dM80o" });

            migrationBuilder.UpdateData(
                table: "LessonVideos",
                keyColumn: "Id",
                keyValue: 4,
                column: "VideoUrl",
                value: "https://youtu.be/UX_So82fEAI");

            migrationBuilder.UpdateData(
                table: "LessonVideos",
                keyColumn: "Id",
                keyValue: 5,
                column: "VideoUrl",
                value: "https://youtu.be/YzfMqUa-Nx0");

            migrationBuilder.UpdateData(
                table: "LessonVideos",
                keyColumn: "Id",
                keyValue: 6,
                column: "VideoUrl",
                value: "https://youtu.be/95SkyJe3Fe0");

            migrationBuilder.UpdateData(
                table: "LessonVideos",
                keyColumn: "Id",
                keyValue: 7,
                column: "VideoUrl",
                value: "https://youtu.be/IzzNzSXkCMM");

            migrationBuilder.UpdateData(
                table: "LessonVideos",
                keyColumn: "Id",
                keyValue: 8,
                column: "VideoUrl",
                value: "https://youtu.be/ySd_-h_Dapc");

            migrationBuilder.UpdateData(
                table: "LessonVideos",
                keyColumn: "Id",
                keyValue: 9,
                column: "VideoUrl",
                value: "https://youtu.be/WhACXlObR8s");

            migrationBuilder.UpdateData(
                table: "LessonVideos",
                keyColumn: "Id",
                keyValue: 10,
                column: "VideoUrl",
                value: "https://youtu.be/IPpEefuFiVM");

            migrationBuilder.UpdateData(
                table: "LessonVideos",
                keyColumn: "Id",
                keyValue: 11,
                column: "VideoUrl",
                value: "https://youtu.be/pTB0EiLXUC8");

            migrationBuilder.UpdateData(
                table: "LessonVideos",
                keyColumn: "Id",
                keyValue: 12,
                column: "VideoUrl",
                value: "https://youtu.be/OoYJy1s4zMY");

            migrationBuilder.UpdateData(
                table: "LessonVideos",
                keyColumn: "Id",
                keyValue: 13,
                column: "VideoUrl",
                value: "https://youtu.be/CClziU97Xeg");

            migrationBuilder.UpdateData(
                table: "LessonVideos",
                keyColumn: "Id",
                keyValue: 14,
                column: "VideoUrl",
                value: "https://youtu.be/AeknkeEUDiI");

            migrationBuilder.UpdateData(
                table: "LessonVideos",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "DurationMinutes", "VideoUrl" },
                values: new object[] { 8, "https://youtu.be/5p415gz2KBY" });

            migrationBuilder.UpdateData(
                table: "LessonVideos",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "DurationMinutes", "VideoUrl" },
                values: new object[] { 10, "https://youtu.be/s1bk-68aB1U" });

            migrationBuilder.UpdateData(
                table: "LessonVideos",
                keyColumn: "Id",
                keyValue: 17,
                column: "VideoUrl",
                value: "https://youtu.be/Hhpq7oYcpGE");

            migrationBuilder.UpdateData(
                table: "LessonVideos",
                keyColumn: "Id",
                keyValue: 18,
                column: "VideoUrl",
                value: "https://youtu.be/Tj3qsKSNvMk");

            migrationBuilder.UpdateData(
                table: "LessonVideos",
                keyColumn: "Id",
                keyValue: 19,
                column: "VideoUrl",
                value: "https://youtu.be/XMUe3zFhM5c");

            migrationBuilder.UpdateData(
                table: "LessonVideos",
                keyColumn: "Id",
                keyValue: 20,
                column: "VideoUrl",
                value: "https://youtu.be/0AO7OwNzd2Y");

            migrationBuilder.UpdateData(
                table: "LessonVideos",
                keyColumn: "Id",
                keyValue: 21,
                column: "VideoUrl",
                value: "https://youtu.be/PBP5KI8OFI0");

            migrationBuilder.UpdateData(
                table: "LessonVideos",
                keyColumn: "Id",
                keyValue: 22,
                column: "VideoUrl",
                value: "https://youtu.be/LlZpno4_ylw");

            migrationBuilder.UpdateData(
                table: "LessonVideos",
                keyColumn: "Id",
                keyValue: 23,
                column: "VideoUrl",
                value: "https://youtu.be/Kf9YiRkj-m4");

            migrationBuilder.UpdateData(
                table: "LessonVideos",
                keyColumn: "Id",
                keyValue: 24,
                column: "VideoUrl",
                value: "https://youtu.be/pgPs_lMAIE4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "LessonVideos",
                keyColumn: "Id",
                keyValue: 1,
                column: "VideoUrl",
                value: "https://www.youtube.com/watch?v=ravLFzWr5H4");

            migrationBuilder.UpdateData(
                table: "LessonVideos",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Title", "VideoUrl" },
                values: new object[] { "Why C# is Amazing — The Big Picture", "https://www.youtube.com/watch?v=M5ugY7fWydE" });

            migrationBuilder.UpdateData(
                table: "LessonVideos",
                keyColumn: "Id",
                keyValue: 4,
                column: "VideoUrl",
                value: "https://www.youtube.com/watch?v=p6bSpbqlNBI");

            migrationBuilder.UpdateData(
                table: "LessonVideos",
                keyColumn: "Id",
                keyValue: 5,
                column: "VideoUrl",
                value: "https://www.youtube.com/watch?v=nrkO3k_gbcM");

            migrationBuilder.UpdateData(
                table: "LessonVideos",
                keyColumn: "Id",
                keyValue: 6,
                column: "VideoUrl",
                value: "https://www.youtube.com/watch?v=9glzO6CCZVI");

            migrationBuilder.UpdateData(
                table: "LessonVideos",
                keyColumn: "Id",
                keyValue: 7,
                column: "VideoUrl",
                value: "https://www.youtube.com/watch?v=y8EZHx4LNDA");

            migrationBuilder.UpdateData(
                table: "LessonVideos",
                keyColumn: "Id",
                keyValue: 8,
                column: "VideoUrl",
                value: "https://www.youtube.com/watch?v=BQHF5VVB4Dc");

            migrationBuilder.UpdateData(
                table: "LessonVideos",
                keyColumn: "Id",
                keyValue: 9,
                column: "VideoUrl",
                value: "https://www.youtube.com/watch?v=3b-VwetPStY");

            migrationBuilder.UpdateData(
                table: "LessonVideos",
                keyColumn: "Id",
                keyValue: 10,
                column: "VideoUrl",
                value: "https://www.youtube.com/watch?v=l9gmFZaP9UE");

            migrationBuilder.UpdateData(
                table: "LessonVideos",
                keyColumn: "Id",
                keyValue: 11,
                column: "VideoUrl",
                value: "https://www.youtube.com/watch?v=pTB0EiLXUC8");

            migrationBuilder.UpdateData(
                table: "LessonVideos",
                keyColumn: "Id",
                keyValue: 12,
                column: "VideoUrl",
                value: "https://www.youtube.com/watch?v=PwDvHVQ8M0g");

            migrationBuilder.UpdateData(
                table: "LessonVideos",
                keyColumn: "Id",
                keyValue: 13,
                column: "VideoUrl",
                value: "https://www.youtube.com/watch?v=wqUfllZyeq4");

            migrationBuilder.UpdateData(
                table: "LessonVideos",
                keyColumn: "Id",
                keyValue: 14,
                column: "VideoUrl",
                value: "https://www.youtube.com/watch?v=rHMsEL90VNs");

            migrationBuilder.UpdateData(
                table: "LessonVideos",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "DurationMinutes", "VideoUrl" },
                values: new object[] { 20, "https://www.youtube.com/watch?v=C-NDfCKwv0I" });

            migrationBuilder.UpdateData(
                table: "LessonVideos",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "DurationMinutes", "VideoUrl" },
                values: new object[] { 60, "https://www.youtube.com/watch?v=AhAxLiGC7Pc" });

            migrationBuilder.UpdateData(
                table: "LessonVideos",
                keyColumn: "Id",
                keyValue: 17,
                column: "VideoUrl",
                value: "https://www.youtube.com/watch?v=Hhpq7oYcpyE");

            migrationBuilder.UpdateData(
                table: "LessonVideos",
                keyColumn: "Id",
                keyValue: 18,
                column: "VideoUrl",
                value: "https://www.youtube.com/watch?v=7PNzEL60YVA");

            migrationBuilder.UpdateData(
                table: "LessonVideos",
                keyColumn: "Id",
                keyValue: 19,
                column: "VideoUrl",
                value: "https://www.youtube.com/watch?v=v4cd1O4zkGw");

            migrationBuilder.UpdateData(
                table: "LessonVideos",
                keyColumn: "Id",
                keyValue: 20,
                column: "VideoUrl",
                value: "https://www.youtube.com/watch?v=WwfhLC16bis");

            migrationBuilder.UpdateData(
                table: "LessonVideos",
                keyColumn: "Id",
                keyValue: 21,
                column: "VideoUrl",
                value: "https://www.youtube.com/watch?v=R8Blt5c-Vi4");

            migrationBuilder.UpdateData(
                table: "LessonVideos",
                keyColumn: "Id",
                keyValue: 22,
                column: "VideoUrl",
                value: "https://www.youtube.com/watch?v=eTuFMr7KZGY");

            migrationBuilder.UpdateData(
                table: "LessonVideos",
                keyColumn: "Id",
                keyValue: 23,
                column: "VideoUrl",
                value: "https://www.youtube.com/watch?v=gwD9awr3NNo");

            migrationBuilder.UpdateData(
                table: "LessonVideos",
                keyColumn: "Id",
                keyValue: 24,
                column: "VideoUrl",
                value: "https://www.youtube.com/watch?v=yh2nGaZvA1o");
        }
    }
}
